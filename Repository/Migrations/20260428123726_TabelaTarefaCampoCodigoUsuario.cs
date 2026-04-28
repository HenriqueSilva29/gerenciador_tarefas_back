using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class TabelaTarefaCampoCodigoUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "idusuario",
                table: "Lembrete");

            migrationBuilder.AddColumn<int>(
                name: "idusuario",
                table: "Tarefa",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tarefa_idusuario",
                table: "Tarefa",
                column: "idusuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefa_Usuario_idusuario",
                table: "Tarefa",
                column: "idusuario",
                principalTable: "Usuario",
                principalColumn: "idusuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefa_Usuario_idusuario",
                table: "Tarefa");

            migrationBuilder.DropIndex(
                name: "IX_Tarefa_idusuario",
                table: "Tarefa");

            migrationBuilder.DropColumn(
                name: "idusuario",
                table: "Tarefa");

            migrationBuilder.AddColumn<int>(
                name: "idusuario",
                table: "Lembrete",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
