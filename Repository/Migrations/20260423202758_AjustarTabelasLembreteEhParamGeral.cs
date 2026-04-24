using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class AjustarTabelasLembreteEhParamGeral : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataVencimento",
                table: "Lembrete");

            migrationBuilder.RenameColumn(
                name: "dias_antes_do_vencimento",
                table: "ParamGeral",
                newName: "tela_inicial");

            migrationBuilder.RenameColumn(
                name: "avisar_vencimento",
                table: "ParamGeral",
                newName: "reforcar_lembrete_no_proprio_dia");

            migrationBuilder.RenameColumn(
                name: "texto",
                table: "Lembrete",
                newName: "descricao");

            migrationBuilder.AddColumn<bool>(
                name: "arquivamento_automatico_tarefas_concluidas",
                table: "ParamGeral",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "arquivar_tarefas_concluidas",
                table: "ParamGeral",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "horario_resumo_diario",
                table: "ParamGeral",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "listagem_padrao_de_tarefas",
                table: "ParamGeral",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "notificar_tarefas_antes_do_inicio",
                table: "ParamGeral",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "primeiro_dia_da_semana",
                table: "ParamGeral",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "quantidade_datetime_antes_do_inicio",
                table: "ParamGeral",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "quantidade_dias_para_arquivamento",
                table: "ParamGeral",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<bool>(
                name: "receber_notificacao_por_email",
                table: "ParamGeral",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "receber_notificacao_por_whatsapp",
                table: "ParamGeral",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "receber_resumo_diario",
                table: "ParamGeral",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "unidade_antes_do_inicio",
                table: "ParamGeral",
                type: "int",
                nullable: false,
                defaultValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "arquivamento_automatico_tarefas_concluidas",
                table: "ParamGeral");

            migrationBuilder.DropColumn(
                name: "arquivar_tarefas_concluidas",
                table: "ParamGeral");

            migrationBuilder.DropColumn(
                name: "horario_resumo_diario",
                table: "ParamGeral");

            migrationBuilder.DropColumn(
                name: "listagem_padrao_de_tarefas",
                table: "ParamGeral");

            migrationBuilder.DropColumn(
                name: "notificar_tarefas_antes_do_inicio",
                table: "ParamGeral");

            migrationBuilder.DropColumn(
                name: "primeiro_dia_da_semana",
                table: "ParamGeral");

            migrationBuilder.DropColumn(
                name: "quantidade_datetime_antes_do_inicio",
                table: "ParamGeral");

            migrationBuilder.DropColumn(
                name: "quantidade_dias_para_arquivamento",
                table: "ParamGeral");

            migrationBuilder.DropColumn(
                name: "receber_notificacao_por_email",
                table: "ParamGeral");

            migrationBuilder.DropColumn(
                name: "receber_notificacao_por_whatsapp",
                table: "ParamGeral");

            migrationBuilder.DropColumn(
                name: "receber_resumo_diario",
                table: "ParamGeral");

            migrationBuilder.DropColumn(
                name: "unidade_antes_do_inicio",
                table: "ParamGeral");

            migrationBuilder.RenameColumn(
                name: "tela_inicial",
                table: "ParamGeral",
                newName: "dias_antes_do_vencimento");

            migrationBuilder.RenameColumn(
                name: "reforcar_lembrete_no_proprio_dia",
                table: "ParamGeral",
                newName: "avisar_vencimento");

            migrationBuilder.RenameColumn(
                name: "descricao",
                table: "Lembrete",
                newName: "texto");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DataVencimento",
                table: "Lembrete",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
