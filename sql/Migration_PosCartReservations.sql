USE SIMS_DB;
GO
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

IF OBJECT_ID('dbo.PosCartReservations', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.PosCartReservations (
        ReservationID   INT IDENTITY(1,1) PRIMARY KEY,
        CreatedBy       INT NOT NULL FOREIGN KEY REFERENCES dbo.Users(UserID),
        CustomerLabel   NVARCHAR(150) NULL,
        CreatedAt       DATETIME NOT NULL DEFAULT GETDATE(),
        InvoiceID       INT NULL FOREIGN KEY REFERENCES dbo.Invoices(InvoiceID)
    );
END;
GO

IF OBJECT_ID('dbo.PosCartReservationLines', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.PosCartReservationLines (
        ReservationID   INT NOT NULL FOREIGN KEY REFERENCES dbo.PosCartReservations(ReservationID),
        ProductID       INT NOT NULL FOREIGN KEY REFERENCES dbo.Products(ProductID),
        ProductName     NVARCHAR(150) NOT NULL,
        UnitPrice       DECIMAL(18,0) NOT NULL CHECK (UnitPrice >= 0),
        Quantity        INT NOT NULL CHECK (Quantity > 0),
        PRIMARY KEY (ReservationID, ProductID)
    );
END;
GO

IF OBJECT_ID('dbo.PosCartReservationBatches', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.PosCartReservationBatches (
        ReservationID   INT NOT NULL,
        ProductID       INT NOT NULL,
        BatchID         INT NOT NULL FOREIGN KEY REFERENCES dbo.InventoryBatch(BatchID),
        Quantity        INT NOT NULL CHECK (Quantity > 0),
        PRIMARY KEY (ReservationID, ProductID, BatchID),
        FOREIGN KEY (ReservationID, ProductID)
            REFERENCES dbo.PosCartReservationLines(ReservationID, ProductID)
    );
END;
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_PosCartReservations_Invoice'
      AND object_id = OBJECT_ID('dbo.PosCartReservations')
)
    CREATE INDEX IX_PosCartReservations_Invoice ON dbo.PosCartReservations(InvoiceID);
GO
