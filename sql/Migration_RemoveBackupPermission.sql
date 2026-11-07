IF OBJECT_ID(N'dbo.RolePermissions', N'U') IS NOT NULL
   AND OBJECT_ID(N'dbo.Permissions', N'U') IS NOT NULL
BEGIN
    DELETE rp
    FROM dbo.RolePermissions AS rp
    INNER JOIN dbo.Permissions AS p
        ON p.PermissionID = rp.PermissionID
    WHERE p.PermissionCode = 'BACKUP_MANAGE';
END;

IF OBJECT_ID(N'dbo.Permissions', N'U') IS NOT NULL
BEGIN
    DELETE FROM dbo.Permissions
    WHERE PermissionCode = 'BACKUP_MANAGE';
END;
GO
