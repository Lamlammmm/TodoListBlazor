using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoList.api.Migrations
{
    /// <inheritdoc />
    public partial class updateTaskNameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoList_AspNetUsers_AssigneeId",
                table: "TodoList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TodoList",
                table: "TodoList");

            migrationBuilder.RenameTable(
                name: "TodoList",
                newName: "Task");

            migrationBuilder.RenameIndex(
                name: "IX_TodoList_AssigneeId",
                table: "Task",
                newName: "IX_Task_AssigneeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Task",
                table: "Task",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_AspNetUsers_AssigneeId",
                table: "Task",
                column: "AssigneeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_AspNetUsers_AssigneeId",
                table: "Task");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Task",
                table: "Task");

            migrationBuilder.RenameTable(
                name: "Task",
                newName: "TodoList");

            migrationBuilder.RenameIndex(
                name: "IX_Task_AssigneeId",
                table: "TodoList",
                newName: "IX_TodoList_AssigneeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TodoList",
                table: "TodoList",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoList_AspNetUsers_AssigneeId",
                table: "TodoList",
                column: "AssigneeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
