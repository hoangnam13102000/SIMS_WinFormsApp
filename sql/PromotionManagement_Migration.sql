/* Idempotent migration for promotion management and POS coupon lookup. */
IF OBJECT_ID('dbo.Promotions', 'U') IS NULL
BEGIN
	CREATE TABLE dbo.Promotions (
		PromotionID INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Promotions PRIMARY KEY,
		Code VARCHAR(30) NOT NULL CONSTRAINT UQ_Promotions_Code UNIQUE,
		Name NVARCHAR(150) NOT NULL,
		DiscountType VARCHAR(10) NOT NULL CONSTRAINT CK_Promotions_DiscountType CHECK (DiscountType IN ('PERCENT', 'AMOUNT')),
		DiscountValue DECIMAL(18,0) NOT NULL CONSTRAINT CK_Promotions_DiscountValue CHECK (DiscountValue > 0),
		MaxDiscountAmount DECIMAL(18,0) NULL,
		MinOrderAmount DECIMAL(18,0) NOT NULL CONSTRAINT DF_Promotions_MinOrderAmount DEFAULT 0,
		StartDate DATE NOT NULL,
		EndDate DATE NOT NULL,
		UsageLimit INT NULL,
		UsedCount INT NOT NULL CONSTRAINT DF_Promotions_UsedCount DEFAULT 0,
		IsActive BIT NOT NULL CONSTRAINT DF_Promotions_IsActive DEFAULT 1,
		ShowOnBanner BIT NOT NULL CONSTRAINT DF_Promotions_ShowOnBanner DEFAULT 0,
		BannerSortOrder INT NULL,
		IsDeleted BIT NOT NULL CONSTRAINT DF_Promotions_IsDeleted DEFAULT 0,
		DeletedAt DATETIME NULL,
		CreatedBy INT NULL,
		CreatedAt DATETIME NOT NULL CONSTRAINT DF_Promotions_CreatedAt DEFAULT GETDATE(),
		CONSTRAINT CK_Promotions_DateRange CHECK (EndDate >= StartDate)
	);
END;
GO

IF COL_LENGTH('dbo.Invoices', 'PromotionID') IS NULL
	ALTER TABLE dbo.Invoices ADD PromotionID INT NULL;
GO
IF COL_LENGTH('dbo.Invoices', 'PromotionCode') IS NULL
	ALTER TABLE dbo.Invoices ADD PromotionCode VARCHAR(30) NULL;
GO

IF OBJECT_ID('dbo.FK_Invoices_Promotion', 'F') IS NULL
	ALTER TABLE dbo.Invoices ADD CONSTRAINT FK_Invoices_Promotion FOREIGN KEY (PromotionID) REFERENCES dbo.Promotions(PromotionID);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Promotions_ActiveRange' AND object_id = OBJECT_ID('dbo.Promotions'))
	CREATE INDEX IX_Promotions_ActiveRange ON dbo.Promotions(IsActive, StartDate, EndDate);
GO

IF OBJECT_ID('dbo.Permissions', 'U') IS NOT NULL
BEGIN
	IF NOT EXISTS (SELECT 1 FROM dbo.Permissions WHERE PermissionCode = 'PROMOTION_MANAGE')
		INSERT INTO dbo.Permissions (PermissionCode, Description)
		VALUES ('PROMOTION_MANAGE', N'Quản lý CRUD mã giảm giá: tạo, xem, sửa và xóa mềm');

	UPDATE dbo.Permissions
	SET Description = N'Quản lý CRUD mã giảm giá: tạo, xem, sửa và xóa mềm'
	WHERE PermissionCode = 'PROMOTION_MANAGE';

	IF EXISTS (SELECT 1 FROM dbo.Roles WHERE RoleCode IN ('ADMIN', 'SALES_MANAGER'))
	   AND EXISTS (SELECT 1 FROM dbo.Permissions WHERE PermissionCode = 'PROMOTION_MANAGE')
	BEGIN
		INSERT INTO dbo.RolePermissions (RoleID, PermissionID)
		SELECT r.RoleID, p.PermissionID
		FROM dbo.Roles r
		CROSS JOIN dbo.Permissions p
		WHERE r.RoleCode IN ('ADMIN', 'SALES_MANAGER')
		  AND p.PermissionCode = 'PROMOTION_MANAGE'
		  AND NOT EXISTS (
			  SELECT 1 FROM dbo.RolePermissions rp
			  WHERE rp.RoleID = r.RoleID AND rp.PermissionID = p.PermissionID);
	END
END;
GO
