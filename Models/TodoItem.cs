using System.ComponentModel.DataAnnotations;

namespace Day2_MvcCore.Models;

public class TodoItem
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tiêu đề không được để trống!")]
    [StringLength(100, ErrorMessage = "Tiêu đề không quá 100 ký tự")]
    public string Title { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Mô tả không quá 500 ký tự")]
    public string Description { get; set; } = string.Empty;

    public bool IsCompleted { get; set; } = false;

    public Priority Priority { get; set; } = Priority.Medium;

    public DateTime? DueDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Foreign Key to Category
    public int? CategoryId { get; set; }

    // Navigation Property
    public Category? Category { get; set; }
}
