using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class adicionarcolunalembrete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "data_envio",
                table: "Lembrete",
                newName: "data_de_envio");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_de_envio",
                table: "Lembrete",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "data_do_envio",
                table: "Lembrete",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "data_do_envio",
                table: "Lembrete");

            migrationBuilder.RenameColumn(
                name: "data_de_envio",
                table: "Lembrete",
                newName: "data_envio");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_envio",
                table: "Lembrete",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
