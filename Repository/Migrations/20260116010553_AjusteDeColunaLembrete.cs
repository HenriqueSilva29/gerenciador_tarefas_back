using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AjusteDeColunaLembrete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "data_de_envio",
                table: "Lembrete");

            migrationBuilder.DropColumn(
                name: "data_do_envio",
                table: "Lembrete");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "data_vencimento",
                table: "ToDoItem",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "data_criacao",
                table: "ToDoItem",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "FoiAgendado",
                table: "Lembrete",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "data_de_agendamento",
                table: "Lembrete",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "data_de_execucao_do_agendamento",
                table: "Lembrete",
                type: "datetimeoffset",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoiAgendado",
                table: "Lembrete");

            migrationBuilder.DropColumn(
                name: "data_de_agendamento",
                table: "Lembrete");

            migrationBuilder.DropColumn(
                name: "data_de_execucao_do_agendamento",
                table: "Lembrete");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_vencimento",
                table: "ToDoItem",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_criacao",
                table: "ToDoItem",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AddColumn<DateTime>(
                name: "data_de_envio",
                table: "Lembrete",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "data_do_envio",
                table: "Lembrete",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
