USE SIMS_DB;
GO

UPDATE Users
SET FullName = CASE Username
    WHEN 'admin' THEN N'Hoàng Trung Nam'
    WHEN 'salesmgr' THEN N'Lê Thị Vân Anh'
    WHEN 'invmgr' THEN N'Hồ Thị Mỹ Thuận'
    WHEN 'staff01' THEN N'Lê Thanh Tú'
END
WHERE Username IN ('admin', 'salesmgr', 'invmgr', 'staff01');
GO
