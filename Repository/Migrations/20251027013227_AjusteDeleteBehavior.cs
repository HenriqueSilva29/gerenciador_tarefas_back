using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AjusteDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CodigoToDoItemPai",
                table: "ToDoItem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItem_CodigoToDoItemPai",
                table: "ToDoItem",
                column: "CodigoToDoItemPai");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItem_ToDoItem_CodigoToDoItemPai",
                table: "ToDoItem",
                column: "CodigoToDoItemPai",
                principalTable: "ToDoItem",
                principalColumn: "idToDoItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItem_ToDoItem_CodigoToDoItemPai",
                table: "ToDoItem");

            migrationBuilder.DropIndex(
                name: "IX_ToDoItem_CodigoToDoItemPai",
                table: "ToDoItem");

            migrationBuilder.DropColumn(
                name: "CodigoToDoItemPai",
                table: "ToDoItem");
        }
    }
}
