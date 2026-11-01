/*
   Reconcile the current carrot stock shown in the product catalog.
   Run Trigger_SIMS.sql first so the reconciliation trigger can reduce
   active batch quantities and record each adjustment.

   Products.Stock is the physical count (248 in the reported example).
   When batch stock is higher, expired batches are reduced first, then
   batches by FEFO order. If batch stock is lower, this script stops:
   surplus stock must be entered as a real receipt/identified batch.
*/
USE SIMS_DB;
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;

IF OBJECT_ID('dbo.trg_StockReconciliation_Apply', 'TR') IS NULL
    THROW 51101, N'Hay chay Trigger_SIMS.sql truoc khi doi soat.', 1;
IF OBJECT_ID('dbo.StockReconciliationBatchAdjustments', 'U') IS NULL
    THROW 51102, N'Thieu bang chi tiet dieu chinh lo. Hay chay Trigger_SIMS.sql truoc.', 1;

DECLARE @ProductCode VARCHAR(50) = 'SP_0004';
DECLARE @ProductID INT;
DECLARE @Stock INT;
DECLARE @BatchStock INT;
DECLARE @CreatedBy INT;

BEGIN TRY
    BEGIN TRANSACTION;

    SELECT @ProductID = ProductID, @Stock = Stock
    FROM dbo.Products WITH (UPDLOCK, HOLDLOCK)
    WHERE ProductCode = @ProductCode;

    IF @ProductID IS NULL
        THROW 51103, N'Khong tim thay ma san pham can doi soat.', 1;

    SELECT @BatchStock = ISNULL(SUM(RemainingQty), 0)
    FROM dbo.InventoryBatch WITH (UPDLOCK, HOLDLOCK)
    WHERE ProductID = @ProductID AND Status = 'ACTIVE';

    IF @Stock = @BatchStock
    BEGIN
        COMMIT TRANSACTION;
        PRINT N'Ton Products.Stock va tong ton cac lo da khop; khong can dieu chinh.';
        RETURN;
    END;

    IF @Stock > @BatchStock
        THROW 51104, N'Ton tong cao hon tong ton lo. Hay tao phieu nhap/lo dieu chinh co du thong tin truoc.', 1;

    SELECT TOP (1) @CreatedBy = u.UserID
    FROM dbo.Users u
    JOIN dbo.Roles r ON r.RoleID = u.RoleID
    WHERE r.RoleCode IN ('INVENTORY_MANAGER', 'ADMIN')
        AND u.Status = 'ACTIVE'
        AND u.IsLocked = 0
        AND u.IsDeleted = 0
    ORDER BY CASE r.RoleCode WHEN 'INVENTORY_MANAGER' THEN 0 ELSE 1 END, u.UserID;

    IF @CreatedBy IS NULL
        THROW 51105, N'Can tai khoan quan ly kho hoac ADMIN dang hoat dong de ghi nhan doi soat.', 1;

    INSERT INTO dbo.StockReconciliation (ProductID, SystemStock, ActualStock, Note, CreatedBy)
    VALUES (
        @ProductID,
        @Stock,
        @Stock,
        N'Doi soat tong ton theo Products.Stock; giam ton lo het han truoc, sau do theo FEFO.',
        @CreatedBy);

    IF EXISTS (
        SELECT 1
        FROM dbo.Products p
        WHERE p.ProductID = @ProductID
          AND p.Stock <> (
              SELECT ISNULL(SUM(b.RemainingQty), 0)
              FROM dbo.InventoryBatch b
              WHERE b.ProductID = p.ProductID AND b.Status = 'ACTIVE')
    )
        THROW 51106, N'Doi soat chua khop sau khi chay trigger; giao dich da bi huy.', 1;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0
        ROLLBACK TRANSACTION;
    THROW;
END CATCH;

SELECT
    p.ProductCode,
    p.ProductName,
    p.Stock AS ProductStock,
    ISNULL(SUM(CASE WHEN b.Status = 'ACTIVE' THEN b.RemainingQty ELSE 0 END), 0) AS ActiveBatchStock
FROM dbo.Products p
LEFT JOIN dbo.InventoryBatch b ON b.ProductID = p.ProductID
WHERE p.ProductID = @ProductID
GROUP BY p.ProductCode, p.ProductName, p.Stock;

SELECT
    r.ReconciliationID,
    b.BatchCode,
    b.ExpiryDate,
    a.Quantity AS AdjustedQuantity,
    a.BatchStockBefore,
    a.BatchStockAfter
FROM dbo.StockReconciliation r
JOIN dbo.StockReconciliationBatchAdjustments a
    ON a.ReconciliationID = r.ReconciliationID
JOIN dbo.InventoryBatch b ON b.BatchID = a.BatchID
WHERE r.ProductID = @ProductID
ORDER BY a.ReconciliationBatchAdjustmentID DESC;
GO
