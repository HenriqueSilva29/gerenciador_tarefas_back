using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class IniciandoTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "auditoria",
                columns: table => new
                {
                    idauditoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    entidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    identidade = table.Column<int>(type: "int", nullable: false),
                    acao = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    idusuario = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    dataocorrencia = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    alteracoes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auditoria", x => x.idauditoria);
                });

            migrationBuilder.CreateTable(
                name: "ParamGeral",
                columns: table => new
                {
                    idparamgeral = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    notificartarefasantesdoinicio = table.Column<bool>(name: "notificar_tarefas_antes_do_inicio", type: "bit", nullable: false, defaultValue: false),
                    quantidadedatetimeantesdoinicio = table.Column<int>(name: "quantidade_datetime_antes_do_inicio", type: "int", nullable: false, defaultValue: 1),
                    unidadeantesdoinicio = table.Column<int>(name: "unidade_antes_do_inicio", type: "int", nullable: false, defaultValue: 2),
                    reforcarlembretenopropriodia = table.Column<bool>(name: "reforcar_lembrete_no_proprio_dia", type: "bit", nullable: false, defaultValue: false),
                    recebernotificacaoporemail = table.Column<bool>(name: "receber_notificacao_por_email", type: "bit", nullable: false, defaultValue: false),
                    recebernotificacaoporwhatsapp = table.Column<bool>(name: "receber_notificacao_por_whatsapp", type: "bit", nullable: false, defaultValue: false),
                    receberresumodiario = table.Column<bool>(name: "receber_resumo_diario", type: "bit", nullable: false, defaultValue: false),
                    horarioresumodiario = table.Column<TimeSpan>(name: "horario_resumo_diario", type: "time", nullable: false),
                    arquivamentoautomaticotarefasconcluidas = table.Column<bool>(name: "arquivamento_automatico_tarefas_concluidas", type: "bit", nullable: false, defaultValue: false),
                    arquivartarefasconcluidas = table.Column<bool>(name: "arquivar_tarefas_concluidas", type: "bit", nullable: false, defaultValue: false),
                    quantidadediasparaarquivamento = table.Column<int>(name: "quantidade_dias_para_arquivamento", type: "int", nullable: false, defaultValue: 1),
                    listagempadraodetarefas = table.Column<int>(name: "listagem_padrao_de_tarefas", type: "int", nullable: false, defaultValue: 0),
                    telainicial = table.Column<int>(name: "tela_inicial", type: "int", nullable: false, defaultValue: 0),
                    primeirodiadasemana = table.Column<int>(name: "primeiro_dia_da_semana", type: "int", nullable: false, defaultValue: 0),
                    email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    telefone = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParamGeral", x => x.idparamgeral);
                });

            migrationBuilder.CreateTable(
                name: "Tarefa",
                columns: table => new
                {
                    idtarefa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    datacriacao = table.Column<DateTimeOffset>(name: "data_criacao", type: "datetimeoffset", nullable: false),
                    datavencimento = table.Column<DateTimeOffset>(name: "data_vencimento", type: "datetimeoffset", nullable: false),
                    datatarefa = table.Column<DateTime>(name: "data_tarefa", type: "datetime2", nullable: false),
                    horainicio = table.Column<TimeSpan>(name: "hora_inicio", type: "time", nullable: false),
                    horafim = table.Column<TimeSpan>(name: "hora_fim", type: "time", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    prioridade = table.Column<int>(type: "int", nullable: false),
                    categoria = table.Column<int>(type: "int", nullable: false),
                    idtarefapai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefa", x => x.idtarefa);
                    table.ForeignKey(
                        name: "FK_Tarefa_Tarefa_idtarefapai",
                        column: x => x.idtarefapai,
                        principalTable: "Tarefa",
                        principalColumn: "idtarefa");
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    idusuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    senhahash = table.Column<string>(name: "senha_hash", type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.idusuario);
                });

            migrationBuilder.CreateTable(
                name: "Lembrete",
                columns: table => new
                {
                    idLembrete = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoTarefa = table.Column<int>(type: "int", nullable: false),
                    DataDisparo = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lembrete", x => x.idLembrete);
                    table.ForeignKey(
                        name: "FK_Lembrete_Tarefa_CodigoTarefa",
                        column: x => x.CodigoTarefa,
                        principalTable: "Tarefa",
                        principalColumn: "idtarefa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_auditoria_data",
                table: "auditoria",
                column: "dataocorrencia");

            migrationBuilder.CreateIndex(
                name: "ix_auditoria_identidade",
                table: "auditoria",
                column: "identidade");

            migrationBuilder.CreateIndex(
                name: "ix_auditoria_usuario",
                table: "auditoria",
                column: "idusuario");

            migrationBuilder.CreateIndex(
                name: "IX_Lembrete_CodigoTarefa",
                table: "Lembrete",
                column: "CodigoTarefa");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefa_idtarefapai",
                table: "Tarefa",
                column: "idtarefapai");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_email",
                table: "Usuario",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "auditoria");

            migrationBuilder.DropTable(
                name: "Lembrete");

            migrationBuilder.DropTable(
                name: "ParamGeral");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Tarefa");
        }
    }
}
