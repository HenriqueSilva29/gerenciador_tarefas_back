using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class TabelaNotificacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lembrete_Tarefa_CodigoTarefa",
                table: "Lembrete");

            migrationBuilder.RenameColumn(
                name: "CodigoTarefa",
                table: "Lembrete",
                newName: "idtarefa");

            migrationBuilder.RenameIndex(
                name: "IX_Lembrete_CodigoTarefa",
                table: "Lembrete",
                newName: "IX_Lembrete_idtarefa");

            migrationBuilder.AddColumn<int>(
                name: "idusuario",
                table: "Lembrete",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Notificacao",
                columns: table => new
                {
                    idnotificacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idusuario = table.Column<int>(type: "int", nullable: true),
                    tipo = table.Column<int>(type: "int", nullable: false),
                    titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    mensagem = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    lida = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    datacriacao = table.Column<DateTimeOffset>(name: "data_criacao", type: "datetimeoffset", nullable: false),
                    dataleitura = table.Column<DateTimeOffset>(name: "data_leitura", type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacao", x => x.idnotificacao);
                    table.ForeignKey(
                        name: "FK_Notificacao_Usuario_idusuario",
                        column: x => x.idusuario,
                        principalTable: "Usuario",
                        principalColumn: "idusuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notificacao_idusuario",
                table: "Notificacao",
                column: "idusuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Lembrete_Tarefa_idtarefa",
                table: "Lembrete",
                column: "idtarefa",
                principalTable: "Tarefa",
                principalColumn: "idtarefa",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lembrete_Tarefa_idtarefa",
                table: "Lembrete");

            migrationBuilder.DropTable(
                name: "Notificacao");

            migrationBuilder.DropColumn(
                name: "idusuario",
                table: "Lembrete");

            migrationBuilder.RenameColumn(
                name: "idtarefa",
                table: "Lembrete",
                newName: "CodigoTarefa");

            migrationBuilder.RenameIndex(
                name: "IX_Lembrete_idtarefa",
                table: "Lembrete",
                newName: "IX_Lembrete_CodigoTarefa");

            migrationBuilder.AddForeignKey(
                name: "FK_Lembrete_Tarefa_CodigoTarefa",
                table: "Lembrete",
                column: "CodigoTarefa",
                principalTable: "Tarefa",
                principalColumn: "idtarefa",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
