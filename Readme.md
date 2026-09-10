# SIMS — HE THONG QUAN LY BAN HANG & KHO
> Sales and Inventory Management System — Ung dung desktop quan ly ban hang va ton kho cho chuoi cua hang Connect Mart, xay dung bang C# Windows Forms (.NET) ket noi SQL Server.

[![C#](https://img.shields.io/badge/C%23-.NET-512BD4?style=for-the-badge&logo=csharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![Windows Forms](https://img.shields.io/badge/Windows_Forms-Desktop_App-0078D4?style=for-the-badge&logo=windows&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-2022+-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/en-us/sql-server)
[![.NET](https://img.shields.io/badge/.NET-6.0+-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Visual Studio](https://img.shields.io/badge/Visual_Studio-2022-5C2D91?style=for-the-badge&logo=visualstudio&logoColor=white)](https://visualstudio.microsoft.com/)
[![BCrypt](https://img.shields.io/badge/BCrypt-Ma_hoa_mat_khau-2C2C2C?style=for-the-badge)](https://www.nuget.org/packages/BCrypt.Net-Next/)
[![EPPlus](https://img.shields.io/badge/EPPlus-Xuat_Excel-4479A1?style=for-the-badge)](https://epplussoftware.com/)
[![ReportViewer](https://img.shields.io/badge/ReportViewer-Bao_cao-7952B3?style=for-the-badge)](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/reportviewer-overview)

---

## MUC LUC
1. [Gioi thieu](#-gioi-thieu)
2. [Cong nghe su dung](#-cong-nghe-su-dung)
3. [Yeu cau he thong](#-yeu-cau-he-thong)
4. [Phan cong cong viec](#-phan-cong-cong-viec)

---

## GIOI THIEU

Sau nhieu nam mo rong chi nhanh, Connect Mart gap kho khan khi quan ly ban hang va kho theo phuong phap thu cong: du lieu sai lech, that thoat hang hoa, thieu bao cao thong ke, khong phan quyen ro rang. SIMS duoc xay dung de giai quyet cac van do do voi cac chuc nang chinh:

- Tu dong hoa quan ly ban hang, hoa don, nhap/xuat kho
- Phan quyen theo vai tro (RBAC): Admin, Quan ly ban hang, Quan ly kho, Nhan vien ban hang
- Kiem tra ton kho tu dong, canh bao so luong toi thieu, xu ly giao dich toan ven
- Bao cao, bieu do thong ke doanh thu, xu huong ton kho
- Ma hoa mat khau + cau hinh ket noi bao mat + giao dien hien dai

---

## CONG NGHE SU DUNG

| Thanh phan | Thong tin |
|---|---|
| Ngon ngu | C# (.NET 6.0 / .NET Framework 4.8) |
| Giao dien | Windows Forms |
| Co so du lieu | Microsoft SQL Server 2022+ (hoac file .mdf LocalDB) |
| Ket noi CSDL | ADO.NET / SqlClient |
| Truy van du lieu | LINQ to SQL / LINQ to Entities |
| Bao mat mat khau | BCrypt.Net (Hash + Salt tu dong) |
| Cau hinh ket noi | App.config + chuoi ket noi duoc ma hoa |
| Bao cao & In an | RDLC / ReportViewer |
| Xuat/Nhap Excel | EPPlus |
| Bieu do thong ke | System.Windows.Forms.DataVisualization.Charting |
| Cong cu phat trien | Visual Studio 2022 |

---

## YEU CAU HE THONG

- Windows 10 / 11 (64-bit)
- Visual Studio 2022 (hoac tuong thich)
- .NET 6.0 Runtime / .NET Framework 4.8 tro len
- SQL Server 2022+ hoac SQL Server Express LocalDB (dinh kem file .mdf)
- Quyen doc/ghi thu muc du an va quyen ket noi CSDL

---

## PHAN CONG CONG VIEC

| Thanh vien | Vai tro | Module chinh phu trach |
|---|---|---|
| Hoang Trung Nam | Truong nhom | Thiet ke CSDL, Auth/RBAC, Quan ly tai khoan & phan quyen, Cau hinh ket noi chung |
|  | Thanh vien |  |
|  | Thanh vien |  |
|  | Thanh vien |  |

**Cong viec chung:** Chuan hoa giao dien, viet tai lieu bao cao, kiem thu tich hop giua cac module.