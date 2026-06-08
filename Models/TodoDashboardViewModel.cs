using System.Collections.Generic;

namespace Day2_MvcCore.Models;

public class TodoDashboardViewModel
{
    // Danh sách các công việc sau khi lọc
    public List<TodoItem> TodoItems { get; set; } = new();

    // Danh sách danh mục phục vụ bộ lọc
    public List<Category> Categories { get; set; } = new();

    // Các chỉ số thống kê (Stats)
    public int TotalTasks { get; set; }
    public int CompletedTasks { get; set; }
    public int PendingTasks { get; set; }
    public int OverdueTasks { get; set; }
    public double CompletionRate { get; set; }

    // Thống kê theo độ ưu tiên
    public Dictionary<Priority, int> TasksByPriority { get; set; } = new();

    // Thống kê theo danh mục
    public Dictionary<string, int> TasksByCategory { get; set; } = new();

    // Các tham số lọc hiện tại (để giữ trạng thái form lọc trên giao diện)
    public string? Search { get; set; }
    public int? CategoryId { get; set; }
    public Priority? Priority { get; set; }
    public string? Status { get; set; } // "all", "completed", "pending"
}
