/*
   Add 50 units to every product through normal purchase receipts.
   The receipt-detail trigger creates inventory batches, updates Products.Stock,
   and records inventory transactions. Expiry is three months from execution.

   Run once after SIMS.sql, Trigger_SIMS.sql, and Insert_SIMS.sql.
*/
USE SIMS_DB;
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;

IF OBJECT_ID('dbo.Products', 'U') IS NULL
    THROW 50001, N'Không tìm thấy bảng Products. Hãy tạo database SIMS_DB trước.', 1;
IF OBJECT_ID('dbo.PurchaseReceiptDetails', 'U') IS NULL
   OR OBJECT_ID('dbo.PurchaseReceipts', 'U') IS NULL
   OR OBJECT_ID('dbo.InventoryBatch', 'U') IS NULL
    THROW 50002, N'Thiếu cấu trúc nhập kho. Hãy chạy SIMS.sql và Trigger_SIMS.sql trước.', 1;
IF OBJECT_ID('dbo.trg_PurchaseReceiptDetails_Insert', 'TR') IS NULL
    THROW 50003, N'Thiếu trigger nhập kho; không thể đồng bộ tồn kho an toàn.', 1;
IF OBJECT_ID('dbo.fn_GetDefaultMargin', 'FN') IS NULL
    THROW 50007, N'Thiếu hàm dbo.fn_GetDefaultMargin. Hãy chạy phần định nghĩa hàm trong Trigger_SIMS.sql trước.', 1;

IF EXISTS (
    SELECT 1
    FROM dbo.PurchaseReceipts
    WHERE ReceiptCode LIKE 'PN-ADD50-V1-%'
)
BEGIN
    PRINT N'Script bổ sung 50 sản phẩm đã được chạy trước đó; không cộng tồn kho lần nữa.';
    RETURN;
END;

DECLARE @CreatedBy INT;
SELECT TOP (1) @CreatedBy = u.UserID
FROM dbo.Users u
JOIN dbo.Roles r ON r.RoleID = u.RoleID
WHERE r.RoleCode IN ('INVENTORY_MANAGER', 'ADMIN')
  AND u.Status = 'ACTIVE'
  AND u.IsLocked = 0
ORDER BY CASE r.RoleCode WHEN 'INVENTORY_MANAGER' THEN 0 ELSE 1 END, u.UserID;

IF @CreatedBy IS NULL
    THROW 50004, N'Cần có tài khoản ADMIN hoặc INVENTORY_MANAGER đang hoạt động để tạo phiếu nhập.', 1;

DECLARE @Items TABLE (
    ProductID INT NOT NULL PRIMARY KEY,
    SupplierID INT NOT NULL,
    ImportPrice DECIMAL(18,0) NOT NULL
);

INSERT INTO @Items (ProductID, SupplierID, ImportPrice)
SELECT p.ProductID, chosen.SupplierID, COALESCE(chosen.SupplyPrice, p.ImportPrice)
FROM dbo.Products p
CROSS APPLY (
    SELECT TOP (1) sp.SupplierID, sp.SupplyPrice
    FROM dbo.SupplierProducts sp
    JOIN dbo.Suppliers s ON s.SupplierID = sp.SupplierID
    WHERE sp.ProductID = p.ProductID
      AND s.IsDeleted = 0
    ORDER BY sp.IsPreferred DESC, sp.SupplierID
) chosen;

IF NOT EXISTS (SELECT 1 FROM @Items)
    THROW 50005, N'Không có sản phẩm nào được liên kết với nhà cung cấp để nhập kho.', 1;

IF EXISTS (
    SELECT 1
    FROM dbo.Products p
    LEFT JOIN @Items i ON i.ProductID = p.ProductID
    WHERE i.ProductID IS NULL
)
    THROW 50006, N'Một hoặc nhiều sản phẩm chưa có nhà cung cấp đang hoạt động. Hãy liên kết nhà cung cấp trước khi nhập kho.', 1;

BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @Receipts TABLE (
        SupplierID INT NOT NULL PRIMARY KEY,
        ReceiptID INT NOT NULL
    );

    INSERT INTO dbo.PurchaseReceipts (ReceiptCode, SupplierID, CreatedBy, TotalAmount)
    OUTPUT inserted.SupplierID, inserted.ReceiptID
        INTO @Receipts (SupplierID, ReceiptID)
    SELECT
        'PN-ADD50-V1-' + RIGHT('000000' + CONVERT(VARCHAR(10), suppliers.SupplierID), 6),
        suppliers.SupplierID,
        @CreatedBy,
        0
    FROM (SELECT DISTINCT SupplierID FROM @Items) suppliers;

    INSERT INTO dbo.PurchaseReceiptDetails
        (ReceiptID, ProductID, Quantity, ImportPrice, LotNumber, ManufactureDate, ExpiryDate)
    SELECT
        r.ReceiptID,
        i.ProductID,
        50,
        i.ImportPrice,
        N'TOP50-V1-' + CONVERT(NVARCHAR(10), i.ProductID),
        CAST(GETDATE() AS DATE),
        DATEADD(MONTH, 3, CAST(GETDATE() AS DATE))
    FROM @Items i
    JOIN @Receipts r ON r.SupplierID = i.SupplierID;

    UPDATE pr
    SET pr.TotalAmount = totals.TotalAmount
    FROM dbo.PurchaseReceipts pr
    JOIN @Receipts r ON r.ReceiptID = pr.ReceiptID
    JOIN (
        SELECT d.ReceiptID, SUM(d.Quantity * d.ImportPrice) AS TotalAmount
        FROM dbo.PurchaseReceiptDetails d
        JOIN @Receipts receiptRows ON receiptRows.ReceiptID = d.ReceiptID
        GROUP BY d.ReceiptID
    ) totals ON totals.ReceiptID = pr.ReceiptID;

    COMMIT TRANSACTION;
    PRINT N'Đã nhập thêm 50 đơn vị cho mỗi sản phẩm; hạn sử dụng sau 3 tháng.';
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0
        ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO
