using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Day2_MvcCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Icon = table.Column<string>(type: "TEXT", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    DueDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Color", "Icon", "Name" },
                values: new object[,]
                {
                    { 1, "#3b82f6", "💼", "Công việc" },
                    { 2, "#8b5cf6", "🎓", "Học tập" },
                    { 3, "#10b981", "🏠", "Cá nhân" },
                    { 4, "#ef4444", "🔥", "Khẩn cấp" }
                });

            migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "DueDate", "IsCompleted", "Priority", "Title" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2026, 6, 6, 9, 0, 0, 0, DateTimeKind.Unspecified), "Đọc tài liệu về luồng dữ liệu giữa Model, View và Controller.", new DateTime(2026, 6, 9, 17, 0, 0, 0, DateTimeKind.Unspecified), true, 2, "Tìm hiểu về MVC là gì?" },
                    { 2, 1, new DateTime(2026, 6, 7, 9, 0, 0, 0, DateTimeKind.Unspecified), "Cài đặt SQLite, tạo AppDbContext, chạy Migration để tạo DB.", new DateTime(2026, 6, 10, 17, 0, 0, 0, DateTimeKind.Unspecified), false, 2, "Cấu hình Entity Framework Core" },
                    { 3, 3, new DateTime(2026, 6, 8, 9, 0, 0, 0, DateTimeKind.Unspecified), "Mua rau quả, sữa và thức ăn cho tuần tới.", new DateTime(2026, 6, 11, 17, 0, 0, 0, DateTimeKind.Unspecified), false, 0, "Mua sắm nhu yếu phẩm" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_CategoryId",
                table: "TodoItems",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoItems");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
