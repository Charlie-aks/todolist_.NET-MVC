using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Day2_MvcCore.Data;
using Day2_MvcCore.Models;

namespace Day2_MvcCore.Controllers;

public class TodoController : Controller
{
    private readonly AppDbContext _context;

    // Inject AppDbContext qua constructor
    public TodoController(AppDbContext context)
    {
        _context = context;
    }

    // ==========================================
    // ACTION: Trang chính hiển thị Dashboard + List Task (có lọc & tìm kiếm)
    // ==========================================
    public async Task<IActionResult> Index(string? search, int? categoryId, Priority? priority, string? status)
    {
        // 1. Lấy toàn bộ danh sách để tính toán chỉ số thống kê (không bị ảnh hưởng bởi bộ lọc danh sách)
        var allTasks = await _context.TodoItems.Include(t => t.Category).ToListAsync();
        var allCategories = await _context.Categories.ToListAsync();

        // 2. Lọc danh sách hiển thị dựa theo điều kiện tìm kiếm từ UI
        var query = _context.TodoItems.Include(t => t.Category).AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(t => t.Title.ToLower().Contains(search.ToLower()) 
                                  || t.Description.ToLower().Contains(search.ToLower()));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(t => t.CategoryId == categoryId.Value);
        }

        if (priority.HasValue)
        {
            query = query.Where(t => t.Priority == priority.Value);
        }

        if (!string.IsNullOrEmpty(status))
        {
            if (status == "completed")
            {
                query = query.Where(t => t.IsCompleted);
            }
            else if (status == "pending")
            {
                query = query.Where(t => !t.IsCompleted);
            }
        }

        var filteredTasks = await query.OrderBy(t => t.IsCompleted)
                                       .ThenByDescending(t => t.Priority)
                                       .ThenBy(t => t.DueDate)
                                       .ToListAsync();

        // 3. Tính toán các thông số thống kê Dashboard
        int total = allTasks.Count;
        int completed = allTasks.Count(t => t.IsCompleted);
        int pending = total - completed;
        int overdue = allTasks.Count(t => !t.IsCompleted && t.DueDate.HasValue && t.DueDate.Value < DateTime.Now);
        double rate = total > 0 ? Math.Round((double)completed / total * 100, 1) : 0;

        // Thống kê theo độ ưu tiên
        var tasksByPriority = new Dictionary<Priority, int>
        {
            { Priority.High, allTasks.Count(t => t.Priority == Priority.High) },
            { Priority.Medium, allTasks.Count(t => t.Priority == Priority.Medium) },
            { Priority.Low, allTasks.Count(t => t.Priority == Priority.Low) }
        };

        // Thống kê số lượng Task theo tên Danh mục
        var tasksByCategory = allCategories.ToDictionary(
            c => c.Name,
            c => allTasks.Count(t => t.CategoryId == c.Id)
        );

        // 4. Đóng gói dữ liệu vào ViewModel
        var viewModel = new TodoDashboardViewModel
        {
            TodoItems = filteredTasks,
            Categories = allCategories,
            TotalTasks = total,
            CompletedTasks = completed,
            PendingTasks = pending,
            OverdueTasks = overdue,
            CompletionRate = rate,
            TasksByPriority = tasksByPriority,
            TasksByCategory = tasksByCategory,
            Search = search,
            CategoryId = categoryId,
            Priority = priority,
            Status = status ?? "all"
        };

        return View(viewModel);
    }

    // ==========================================
    // ACTION: Trả về View tạo mới
    // ==========================================
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var categories = await _context.Categories.ToListAsync();
        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        return View();
    }

    // ==========================================
    // ACTION: Xử lý lưu Task mới tạo
    // ==========================================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TodoItem newTodo)
    {
        if (ModelState.IsValid)
        {
            newTodo.CreatedAt = DateTime.Now;
            _context.TodoItems.Add(newTodo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        var categories = await _context.Categories.ToListAsync();
        ViewBag.Categories = new SelectList(categories, "Id", "Name", newTodo.CategoryId);
        return View(newTodo);
    }

    // ==========================================
    // ACTION: Trả về View chỉnh sửa Task
    // ==========================================
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var todo = await _context.TodoItems.FindAsync(id);
        if (todo == null)
        {
            return NotFound();
        }

        var categories = await _context.Categories.ToListAsync();
        ViewBag.Categories = new SelectList(categories, "Id", "Name", todo.CategoryId);
        return View(todo);
    }

    // ==========================================
    // ACTION: Xử lý lưu thay đổi chỉnh sửa
    // ==========================================
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TodoItem updatedTodo)
    {
        if (id != updatedTodo.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(updatedTodo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TodoItems.Any(t => t.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        var categories = await _context.Categories.ToListAsync();
        ViewBag.Categories = new SelectList(categories, "Id", "Name", updatedTodo.CategoryId);
        return View(updatedTodo);
    }

    // ==========================================
    // ACTION: Đánh dấu Hoàn thành / Chưa hoàn thành nhanh
    // ==========================================
    [HttpPost]
    public async Task<IActionResult> Toggle(int id)
    {
        var todo = await _context.TodoItems.FindAsync(id);
        if (todo != null)
        {
            todo.IsCompleted = !todo.IsCompleted;
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    // ==========================================
    // ACTION: Xóa Task
    // ==========================================
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var todo = await _context.TodoItems.FindAsync(id);
        if (todo != null)
        {
            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
