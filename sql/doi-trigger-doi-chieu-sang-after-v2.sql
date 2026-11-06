/* ============================================================
   Doi trg_StockReconciliation_Apply tu INSTEAD OF INSERT sang AFTER INSERT
   (phien ban khop voi sql/Trigger_SIMS.sql moi nhat cua nhanh main)

   Ly do: LINQ to SQL lay ID dong moi bang SCOPE_IDENTITY(). Voi INSTEAD OF
   trigger, dong duoc INSERT ben trong trigger nen SCOPE_IDENTITY() ben ngoai
   tra NULL -> loi "Member AutoSync failure ...".

   Nghiep vu giu nguyen:
   - SystemStock luon tinh lai tu Products.Stock tai thoi diem ghi
   - ActualStock > tong ton lo ACTIVE -> bao loi 51003
   - Neu tong ton lo > ActualStock: giam lo het han truoc, sau do FEFO,
     ghi StockReconciliationBatchAdjustments
   - Neu Diff <> 0: chinh Products.Stock + ghi InventoryTransactions RECONCILE_ADJUST
   ============================================================ */
USE SIMS_DB;
GO

DROP TRIGGER IF EXISTS dbo.trg_StockReconciliation_Apply;
GO

CREATE TRIGGER dbo.trg_StockReconciliation_Apply
ON dbo.StockReconciliation
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ReconciliationID INT, @ProductID INT, @ActualStock INT, @Note NVARCHAR(255), @CreatedBy INT;
    DECLARE @SystemStock INT, @BatchStock INT;
    DECLARE @Diff INT, @BatchDiff INT, @BatchID INT, @BatchRemain INT;
    DECLARE @Take INT, @BatchStockBefore INT, @BatchStockAfter INT;

    DECLARE recon_cursor CURSOR LOCAL FAST_FORWARD FOR
        SELECT ReconciliationID, ProductID, ActualStock, Note, CreatedBy
        FROM inserted
        ORDER BY ReconciliationID;

    OPEN recon_cursor;
    FETCH NEXT FROM recon_cursor INTO @ReconciliationID, @ProductID, @ActualStock, @Note, @CreatedBy;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @SystemStock = NULL;
        SELECT @SystemStock = Stock FROM dbo.Products WHERE ProductID = @ProductID;

        IF @SystemStock IS NULL
            THROW 51001, N'San pham khong ton tai, khong the doi chieu kho.', 1;
        IF @ActualStock < 0
            THROW 51002, N'Ton kho kiem ke khong duoc am.', 1;

        SELECT @BatchStock = ISNULL(SUM(RemainingQty), 0)
        FROM dbo.InventoryBatch WITH (UPDLOCK, HOLDLOCK)
        WHERE ProductID = @ProductID AND Status = 'ACTIVE';

        IF @ActualStock > @BatchStock
            THROW 51003, N'Ton kiem ke cao hon tong ton cac lo. Hay tao phieu nhap/lo dieu chinh co du thong tin truoc khi doi chieu.', 1;

        -- Ghi lai ton he thong tai thoi diem doi chieu (bo qua gia tri app gui len)
        UPDATE dbo.StockReconciliation
        SET SystemStock = @SystemStock
        WHERE ReconciliationID = @ReconciliationID;

        SET @BatchDiff = @BatchStock - @ActualStock;
        IF @BatchDiff > 0
        BEGIN
            DECLARE batch_cursor CURSOR LOCAL FAST_FORWARD FOR
                SELECT BatchID, RemainingQty
                FROM dbo.InventoryBatch WITH (UPDLOCK, ROWLOCK)
                WHERE ProductID = @ProductID
                    AND Status = 'ACTIVE'
                    AND RemainingQty > 0
                ORDER BY
                    CASE WHEN ExpiryDate < CONVERT(DATE, GETDATE()) THEN 0 ELSE 1 END,
                    ISNULL(ExpiryDate, CONVERT(DATE, '99991231')),
                    BatchID;

            OPEN batch_cursor;
            FETCH NEXT FROM batch_cursor INTO @BatchID, @BatchRemain;

            WHILE @@FETCH_STATUS = 0 AND @BatchDiff > 0
            BEGIN
                SET @Take = CASE WHEN @BatchRemain < @BatchDiff
                                 THEN @BatchRemain ELSE @BatchDiff END;
                SET @BatchStockBefore = @BatchRemain;
                SET @BatchStockAfter = @BatchRemain - @Take;

                UPDATE dbo.InventoryBatch
                SET RemainingQty = @BatchStockAfter,
                    Status = CASE WHEN @BatchStockAfter = 0 THEN 'DEPLETED' ELSE Status END
                WHERE BatchID = @BatchID
                    AND ProductID = @ProductID
                    AND Status = 'ACTIVE'
                    AND RemainingQty = @BatchStockBefore;

                IF @@ROWCOUNT <> 1
                    THROW 51004, N'So luong lo vua thay doi. Giao dich doi chieu da bi huy; hay tai lai va thu lai.', 1;

                INSERT INTO dbo.StockReconciliationBatchAdjustments
                    (ReconciliationID, BatchID, Quantity, BatchStockBefore, BatchStockAfter)
                VALUES
                    (@ReconciliationID, @BatchID, @Take, @BatchStockBefore, @BatchStockAfter);

                SET @BatchDiff -= @Take;
                FETCH NEXT FROM batch_cursor INTO @BatchID, @BatchRemain;
            END

            CLOSE batch_cursor;
            DEALLOCATE batch_cursor;

            IF @BatchDiff > 0
                THROW 51005, N'Khong the phan bo chenh lech vao cac lo dang hoat dong.', 1;
        END

        SET @Diff = @ActualStock - @SystemStock;
        IF @Diff <> 0
        BEGIN
            UPDATE dbo.Products SET Stock = @ActualStock WHERE ProductID = @ProductID;

            INSERT INTO dbo.InventoryTransactions (ProductID, TransactionType, Direction, Quantity,
                                                   StockBefore, StockAfter, RefTable, RefID, CreatedBy, Note)
            VALUES (@ProductID, 'RECONCILE_ADJUST',
                    CASE WHEN @Diff > 0 THEN 'IN' ELSE 'OUT' END,
                    ABS(@Diff), @SystemStock, @ActualStock,
                    'StockReconciliation', @ReconciliationID, @CreatedBy, @Note);
        END

        FETCH NEXT FROM recon_cursor INTO @ReconciliationID, @ProductID, @ActualStock, @Note, @CreatedBy;
    END

    CLOSE recon_cursor;
    DEALLOCATE recon_cursor;
END;
GO

-- Kiem tra: is_instead_of_trigger phai = 0
SELECT name, is_instead_of_trigger FROM sys.triggers WHERE name = 'trg_StockReconciliation_Apply';
GO
