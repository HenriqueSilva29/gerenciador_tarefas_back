using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class ajustarTabelaLembrete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "antecedencia",
                table: "Lembrete");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DataDeVencimento",
                table: "Lembrete",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "diasAntesDoVencimento",
                table: "Lembrete",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataDeVencimento",
                table: "Lembrete");

            migrationBuilder.DropColumn(
                name: "diasAntesDoVencimento",
                table: "Lembrete");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "antecedencia",
                table: "Lembrete",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
