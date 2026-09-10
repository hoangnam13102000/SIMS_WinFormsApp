# SIMS — HỆ THỐNG QUẢN LÝ BÁN HÀNG & KHO
> Sales and Inventory Management System — Ứng dụng desktop quản lý bán hàng và tồn kho cho chuỗi cửa hàng Connect Mart, xây dựng bằng C# Windows Forms (.NET) kết nối SQL Server.

[![C#](https://img.shields.io/badge/C%23-.NET-512BD4?style=for-the-badge&logo=csharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![Windows Forms](https://img.shields.io/badge/Windows_Forms-Desktop_App-0078D4?style=for-the-badge&logo=windows&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-2022+-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/en-us/sql-server)
[![.NET](https://img.shields.io/badge/.NET-6.0+-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Visual Studio](https://img.shields.io/badge/Visual_Studio-2022-5C2D91?style=for-the-badge&logo=visualstudio&logoColor=white)](https://visualstudio.microsoft.com/)
[![BCrypt](https://img.shields.io/badge/BCrypt-Mã_hóa_mật_khẩu-2C2C2C?style=for-the-badge)](https://www.nuget.org/packages/BCrypt.Net-Next/)
[![EPPlus](https://img.shields.io/badge/EPPlus-Xuất_Excel-4479A1?style=for-the-badge)](https://epplussoftware.com/)
[![ReportViewer](https://img.shields.io/badge/ReportViewer-Báo_cáo-7952B3?style=for-the-badge)](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/reportviewer-overview)

---

## MỤC LỤC
1. [Giới thiệu](#-giới-thiệu)
2. [Công nghệ sử dụng](#-công-nghệ-sử-dụng)
3. [Yêu cầu hệ thống](#-yêu-cầu-hệ-thống)
4. [Phân công công việc](#-phân-công-công-việc)

---

## GIỚI THIỆU

Sau nhiều năm mở rộng chi nhánh, Connect Mart gặp khó khăn khi quản lý bán hàng và kho theo phương pháp thủ công: dữ liệu sai lệch, thất thoát hàng hóa, thiếu báo cáo thống kê, không phân quyền rõ ràng. SIMS được xây dựng để giải quyết các vấn đề đó với các chức năng chính:

- Tự động hóa quản lý bán hàng, hóa đơn, nhập/xuất kho
- Phân quyền theo vai trò (RBAC): Admin, Quản lý bán hàng, Quản lý kho, Nhân viên bán hàng
- Kiểm tra tồn kho tự động, cảnh báo số lượng tối thiểu, xử lý giao dịch toàn vẹn
- Báo cáo, biểu đồ thống kê doanh thu, xu hướng tồn kho
- Mã hóa mật khẩu + cấu hình kết nối bảo mật + giao diện hiện đại

---

## CÔNG NGHỆ SỬ DỤNG

| Thành phần | Thông tin |
|---|---|
| Ngôn ngữ | C# (.NET 6.0 / .NET Framework 4.8) |
| Giao diện | Windows Forms |
| Cơ sở dữ liệu | Microsoft SQL Server 2022+ (hoặc file .mdf LocalDB) |
| Kết nối CSDL | ADO.NET / SqlClient |
| Truy vấn dữ liệu | LINQ to SQL / LINQ to Entities |
| Bảo mật mật khẩu | BCrypt.Net (Hash + Salt tự động) |
| Cấu hình kết nối | App.config + chuỗi kết nối được mã hóa |
| Báo cáo & In ấn | RDLC / ReportViewer |
| Xuất/Nhập Excel | EPPlus |
| Biểu đồ thống kê | System.Windows.Forms.DataVisualization.Charting |
| Công cụ phát triển | Visual Studio 2022 |

---

## YÊU CẦU HỆ THỐNG

- Windows 10 / 11 (64-bit)
- Visual Studio 2022 (hoặc tương thích)
- .NET 6.0 Runtime / .NET Framework 4.8 trở lên
- SQL Server 2022+ hoặc SQL Server Express LocalDB (đính kèm file .mdf)
- Quyền đọc/ghi thư mục dự án và quyền kết nối CSDL

---

## PHÂN CÔNG CÔNG VIỆC

| Thành viên | Vai trò | Module chính phụ trách |
|---|---|---|
| Hoàng Trung Nam | Trưởng nhóm | Thiết kế CSDL, Auth/RBAC, Quản lý tài khoản & phân quyền, Cấu hình kết nối chung |
|  | Thành viên |  |
|  | Thành viên |  |
|  | Thành viên |  |

**Công việc chung:** Chuẩn hóa giao diện, viết tài liệu báo cáo, kiểm thử tích hợp giữa các module.