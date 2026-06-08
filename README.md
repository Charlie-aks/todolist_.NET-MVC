# 🚀 Task Flow - Personal Productivity & Task Management Dashboard

**Task Flow** là một ứng dụng Dashboard quản lý công việc và hiệu suất cá nhân được phát triển bằng **ASP.NET Core MVC (net10.0)** kết hợp **Entity Framework Core** và cơ sở dữ liệu **SQLite**. Dự án được xây dựng từ một ứng dụng Todo cơ bản nhằm mục tiêu tối ưu hóa trải nghiệm người dùng với giao diện hiện đại và cấu trúc cơ sở dữ liệu thực tế.

---

## 📸 Giao diện ứng dụng (UI/UX)
Ứng dụng được thiết kế theo phong cách **Glassmorphism (Hiệu ứng kính mờ)** thời thượng với các bảng điều khiển trực quan:
* Huy hiệu (Badges) phân loại độ ưu tiên màu sắc tự động.
* Bảng phân tích phân phối công việc theo Danh mục & Độ ưu tiên dạng thanh tiến trình (progress bars).
* Giao diện Responsive hiển thị hoàn hảo trên cả máy tính và di động.

---

## ✨ Các tính năng nổi bật (Key Features)

### 1. Phân tích & Thống kê Chỉ số (Dashboard Analytics)
* Hiển thị thời gian thực các số liệu: **Tổng số công việc**, **Đã hoàn thành**, **Đang thực hiện** và **Số công việc bị trễ hạn**.
* Thanh tiến trình tính toán động **Tỷ lệ hoàn thành công việc (Completion Rate)**.

### 2. Quản lý Danh mục & Độ ưu tiên (Relational Database)
* Thiết lập mối quan hệ **1-nhiều (One-to-Many)** giữa bảng `Categories` (Danh mục: Công việc, Học tập, Cá nhân, Khẩn cấp...) và bảng `TodoItems`.
* Hỗ trợ gán **Mức độ ưu tiên (Priority)**: Cao (🔴), Trung bình (🟡), Thấp (🔵) cùng hạn chót hoàn thành (**Due Date**).

### 3. Bộ lọc & Tìm kiếm đa tiêu chí (Advanced Search & Filter)
* Tìm kiếm từ khóa theo Tiêu đề hoặc Mô tả công việc.
* Bộ lọc kết hợp: Lọc theo Danh mục, Mức độ ưu tiên, hoặc Trạng thái công việc (Đang thực hiện/Đã hoàn thành).

### 4. Đảm bảo an toàn dữ liệu & Kiểm lỗi (Security & Validation)
* Áp dụng cơ chế bảo mật chống giả mạo request **`[ValidateAntiForgeryToken]`** trên mọi Form hành động.
* Ràng buộc dữ liệu nghiêm ngặt sử dụng **Model Validation** (`[Required]`, `[StringLength]`) ở cả phía client và server.

---

## 🛠️ Công nghệ sử dụng (Tech Stack)

* **Framework**: ASP.NET Core MVC (.NET 10.0)
* **ORM**: Entity Framework Core 10.0 (Code-First approach)
* **Database**: SQLite Database (Nhẹ, lưu trữ trực tiếp dưới dạng file tuần tự trong dự án)
* **Frontend**: HTML5, CSS3 (Custom Glassmorphism, Google Fonts Outfit & Plus Jakarta Sans), Bootstrap 5.

---

## 📁 Cấu trúc thư mục dự án

```text
Day2_MvcCore/
│
├── Controllers/
│   ├── TodoController.cs        # Xử lý luồng nghiệp vụ & tính toán Analytics
│   └── HomeController.cs        # Điều phối trang mặc định
│
├── Models/
│   ├── TodoItem.cs              # Model Công việc (Task)
│   ├── Category.cs              # Model Danh mục (Category)
│   ├── Priority.cs              # Enum Mức độ ưu tiên
│   └── TodoDashboardViewModel.cs# ViewModel đóng gói dữ liệu cho Dashboard
│
├── Data/
│   └── AppDbContext.cs          # Cấu hình EF Core, liên kết bảng & Seed Data mẫu
│
├── Views/
│   ├── Todo/
│   │   ├── Index.cshtml         # Giao diện chính Dashboard & Danh sách Task
│   │   ├── Create.cshtml        # Form thêm mới công việc nâng cấp
│   │   └── Edit.cshtml          # Form cập nhật công việc
│   └── Shared/_Layout.cshtml    # Khung layout chung
│
├── wwwroot/
│   ├── css/site.css             # Hệ thống CSS thiết kế Glassmorphism
│   └── todo.db                  # Database SQLite vật lý
│
└── Program.cs                   # Điểm khởi đầu & Đăng ký Services (DbContext)
```

---

## 🚀 Hướng dẫn cài đặt và chạy ứng dụng

### Yêu cầu hệ thống:
* Đã cài đặt **.NET 10 SDK** trở lên.
* Công cụ terminal (PowerShell / Git Bash) hoặc IDE (VS Code / Visual Studio 2022).

### Các bước thực hiện:

1. **Clone project về máy:**
   ```bash
   git clone https://github.com/Charlie-aks/todolist_.NET-MVC.git
   cd todolist_.NET-MVC
   ```

2. **Khôi phục các thư viện NuGet:**
   ```bash
   dotnet restore
   ```

3. **Cập nhật Database (SQLite):**
   *(Nếu file `todo.db` chưa được tạo hoặc bạn muốn tạo mới từ đầu)*
   ```bash
   dotnet ef database update
   ```

4. **Khởi chạy ứng dụng:**
   ```bash
   dotnet run --launch-profile http
   ```

5. **Trải nghiệm ứng dụng:**
   Mở trình duyệt web bất kỳ và truy cập đường dẫn: **`http://localhost:5276`**

---

## 💡 Bài học kinh nghiệm & Điểm cộng trong CV
* Hiểu và áp dụng kiến trúc **MVC (Model - View - Controller)** trong dự án thực tế.
* Thành thạo cách kết nối cơ sở dữ liệu quan hệ bằng **EF Core Code-First**, viết các migrations và seeding dữ liệu tĩnh.
* Kỹ năng viết câu lệnh **LINQ** để lọc dữ liệu phức tạp và gom nhóm thống kê báo cáo (Aggregations).
* Thiết kế CSS UI hiện đại không phụ thuộc hoàn toàn vào UI frameworks lớn, nâng cao tư duy UI/UX.
