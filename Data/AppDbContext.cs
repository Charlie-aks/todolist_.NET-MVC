using Microsoft.EntityFrameworkCore;
using Day2_MvcCore.Models;

namespace Day2_MvcCore.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<TodoItem> TodoItems { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Cấu hình quan hệ 1-nhiều giữa Category và TodoItem
        modelBuilder.Entity<TodoItem>()
            .HasOne(t => t.Category)
            .WithMany(c => c.TodoItems)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        // Seed data mặc định cho Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Công việc", Icon = "💼", Color = "#3b82f6" }, // Blue
            new Category { Id = 2, Name = "Học tập", Icon = "🎓", Color = "#8b5cf6" },   // Purple
            new Category { Id = 3, Name = "Cá nhân", Icon = "🏠", Color = "#10b981" },  // Green
            new Category { Id = 4, Name = "Khẩn cấp", Icon = "🔥", Color = "#ef4444" }   // Red
        );

        // Seed data mặc định cho TodoItems để chạy thử app
        modelBuilder.Entity<TodoItem>().HasData(
            new TodoItem 
            { 
                Id = 1, 
                Title = "Tìm hiểu về MVC là gì?", 
                Description = "Đọc tài liệu về luồng dữ liệu giữa Model, View và Controller.",
                IsCompleted = true, 
                Priority = Priority.High,
                CategoryId = 2, // Học tập
                DueDate = new DateTime(2026, 6, 9, 17, 0, 0),
                CreatedAt = new DateTime(2026, 6, 6, 9, 0, 0)
            },
            new TodoItem 
            { 
                Id = 2, 
                Title = "Cấu hình Entity Framework Core", 
                Description = "Cài đặt SQLite, tạo AppDbContext, chạy Migration để tạo DB.",
                IsCompleted = false, 
                Priority = Priority.High,
                CategoryId = 1, // Công việc
                DueDate = new DateTime(2026, 6, 10, 17, 0, 0),
                CreatedAt = new DateTime(2026, 6, 7, 9, 0, 0)
            },
            new TodoItem 
            { 
                Id = 3, 
                Title = "Mua sắm nhu yếu phẩm", 
                Description = "Mua rau quả, sữa và thức ăn cho tuần tới.",
                IsCompleted = false, 
                Priority = Priority.Low,
                CategoryId = 3, // Cá nhân
                DueDate = new DateTime(2026, 6, 11, 17, 0, 0),
                CreatedAt = new DateTime(2026, 6, 8, 9, 0, 0)
            }
        );
    }
}
