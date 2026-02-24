using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToDoItem",
                columns: table => new
                {
                    idtodoitem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    datacriacao = table.Column<DateTimeOffset>(name: "data_criacao", type: "datetimeoffset", nullable: false),
                    datavencimento = table.Column<DateTimeOffset>(name: "data_vencimento", type: "datetimeoffset", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    prioridade = table.Column<int>(type: "int", nullable: false),
                    categoria = table.Column<int>(type: "int", nullable: false),
                    idtodoitempai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoItem", x => x.idtodoitem);
                    table.ForeignKey(
                        name: "FK_ToDoItem_ToDoItem_idtodoitempai",
                        column: x => x.idtodoitempai,
                        principalTable: "ToDoItem",
                        principalColumn: "idtodoitem");
                });

            migrationBuilder.CreateTable(
                name: "Lembrete",
                columns: table => new
                {
                    idLembrete = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoToDoItem = table.Column<int>(type: "int", nullable: false),
                    DataVencimento = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DataDisparo = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItem_idtodoitempai",
                table: "ToDoItem",
                column: "idtodoitempai");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lembrete");

            migrationBuilder.DropTable(
                name: "ToDoItem");
        }
    }
}
