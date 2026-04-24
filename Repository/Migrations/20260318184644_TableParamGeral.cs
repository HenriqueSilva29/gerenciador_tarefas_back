using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class TableParamGeral : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParamGeral",
                columns: table => new
                {
                    idparamgeral = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    avisarvencimento = table.Column<bool>(name: "avisar_vencimento", type: "bit", nullable: false),
                    diasantesdovencimento = table.Column<int>(name: "dias_antes_do_vencimento", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParamGeral", x => x.idparamgeral);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParamGeral");
        }
    }
}
