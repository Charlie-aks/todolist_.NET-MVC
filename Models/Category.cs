using System.ComponentModel.DataAnnotations;

namespace Day2_MvcCore.Models;

public class Category
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên danh mục không được để trống!")]
    [StringLength(50, ErrorMessage = "Tên danh mục không quá 50 ký tự")]
    public string Name { get; set; } = string.Empty;

    public string Icon { get; set; } = "📁"; // E.g. "💼", "🏠", "🎓"

    public string Color { get; set; } = "#6366f1"; // Default purple hex color

    // Navigation property - Một danh mục có nhiều công việc
    public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
}
