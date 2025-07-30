# 🧳 Ứng Dụng Quản Lý Tour Du Lịch

Ứng dụng Quản Lý Tour Du Lịch là một ứng dụng di động được phát triển bằng **.NET MAUI**, hỗ trợ người dùng tìm kiếm, đặt tour du lịch và quản lý thông tin cá nhân. Ứng dụng có hai vai trò:

- 👤 **Người dùng**:
  - Tìm kiếm và xem chi tiết tour.
  - Đặt tour du lịch.
  - Quản lý danh sách tour yêu thích.
  - Cập nhật hồ sơ cá nhân.

- 🛠️ **Quản trị viên**:
  - Thêm, sửa, xóa tour du lịch.
  - Xem , quản lý danh sách tour đã được đặt.
  - Quản lý tài khoản người dùng.
 
## 📘 Tài liệu liên quan

- [📄 Báo cáo chi tiết các tính năng và giao diện](https://example.com/duong-dan-bao-cao)

---

Ứng dụng sử dụng **SQLite** làm cơ sở dữ liệu cục bộ, đảm bảo hiệu suất tốt và truy cập nhanh.

---

## 🚀 Tính năng chính

- 🔍 Tìm kiếm tour theo từ khóa
- 📝 Đặt tour và xem lịch sử đặt chỗ
- ❤️ Lưu tour yêu thích
- 📱 Giao diện thân thiện, điều hướng mượt mà
- 🔐 Đăng nhập, đăng ký, quản lý người dùng

---

## 🛠️ Công nghệ sử dụng

- .NET MAUI (.NET 8.0+)
- SQLite-net-pcl
- MVVM pattern
- Dependency Injection (Microsoft.Extensions.DependencyInjection)

---

## 💻 Yêu cầu hệ thống

- **Hệ điều hành**: Windows / macOS / Linux (cho phát triển), Android / iOS (cho triển khai)
- **Công cụ**:
  - Visual Studio 2022 (có cài .NET MAUI workload) hoặc VS Code
  - Android SDK / Xcode (tuỳ nền tảng)

---

## 📦 Hướng dẫn cài đặt

```bash
# Clone repository
git clone https://github.com/mv-thuyen2004/NetProjectTour.git
cd NetProjectTour

# Khởi chạy bằng Visual Studio hoặc dùng lệnh:
dotnet build
dotnet run
