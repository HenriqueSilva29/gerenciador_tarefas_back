using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class RenomearColunasToDoItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItem_ToDoItem_CodigoToDoItemPai",
                table: "ToDoItem");

            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "ToDoItem",
                newName: "titulo");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "ToDoItem",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Prioridade",
                table: "ToDoItem",
                newName: "prioridade");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "ToDoItem",
                newName: "descricao");

            migrationBuilder.RenameColumn(
                name: "Categoria",
                table: "ToDoItem",
                newName: "categoria");

            migrationBuilder.RenameColumn(
                name: "idToDoItem",
                table: "ToDoItem",
                newName: "idtodoitem");

            migrationBuilder.RenameColumn(
                name: "DataVencimento",
                table: "ToDoItem",
                newName: "data_vencimento");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "ToDoItem",
                newName: "data_criacao");

            migrationBuilder.RenameColumn(
                name: "CodigoToDoItemPai",
                table: "ToDoItem",
                newName: "idtodoitempai");

            migrationBuilder.RenameIndex(
                name: "IX_ToDoItem_CodigoToDoItemPai",
                table: "ToDoItem",
                newName: "IX_ToDoItem_idtodoitempai");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_vencimento",
                table: "ToDoItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Lembrete",
                columns: table => new
                {
                    idLembrete = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoToDoItem = table.Column<int>(type: "int", nullable: false),
                    antecedencia = table.Column<TimeSpan>(type: "time", nullable: false),
                    dataenvio = table.Column<DateTime>(name: "data_envio", type: "datetime2", nullable: true),
                    texto = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lembrete", x => x.idLembrete);
                    table.ForeignKey(
                        name: "FK_Lembrete_ToDoItem_CodigoToDoItem",
                        column: x => x.CodigoToDoItem,
                        principalTable: "ToDoItem",
                        principalColumn: "idtodoitem",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lembrete_CodigoToDoItem",
                table: "Lembrete",
                column: "CodigoToDoItem");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItem_ToDoItem_idtodoitempai",
                table: "ToDoItem",
                column: "idtodoitempai",
                principalTable: "ToDoItem",
                principalColumn: "idtodoitem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItem_ToDoItem_idtodoitempai",
                table: "ToDoItem");

            migrationBuilder.DropTable(
                name: "Lembrete");

            migrationBuilder.RenameColumn(
                name: "titulo",
                table: "ToDoItem",
                newName: "Titulo");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "ToDoItem",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "prioridade",
                table: "ToDoItem",
                newName: "Prioridade");

            migrationBuilder.RenameColumn(
                name: "descricao",
                table: "ToDoItem",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "categoria",
                table: "ToDoItem",
                newName: "Categoria");

            migrationBuilder.RenameColumn(
                name: "idtodoitem",
                table: "ToDoItem",
                newName: "idToDoItem");

            migrationBuilder.RenameColumn(
                name: "idtodoitempai",
                table: "ToDoItem",
                newName: "CodigoToDoItemPai");

            migrationBuilder.RenameColumn(
                name: "data_vencimento",
                table: "ToDoItem",
                newName: "DataVencimento");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "ToDoItem",
                newName: "DataCriacao");

            migrationBuilder.RenameIndex(
                name: "IX_ToDoItem_idtodoitempai",
                table: "ToDoItem",
                newName: "IX_ToDoItem_CodigoToDoItemPai");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataVencimento",
                table: "ToDoItem",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItem_ToDoItem_CodigoToDoItemPai",
                table: "ToDoItem",
                column: "CodigoToDoItemPai",
                principalTable: "ToDoItem",
                principalColumn: "idToDoItem");
        }
    }
}
