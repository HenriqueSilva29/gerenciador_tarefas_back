using Domain.Entidades;

namespace Application.Emails
{
    public class LembreteEmailCompose
    {
        public EmailMessage Compose(Lembrete lembrete, ParamGeral paramGeral)
        {
            var unidade = ObterUnidadeTexto(paramGeral.Unidade);

            return new EmailMessage
            {
                To = paramGeral.Email,
                Subject = $"Lembrete: {lembrete.Tarefa.Titulo}",
                Body = $"""
                <p>Título: {lembrete.Tarefa.Titulo} </p>
                <p><strong>Descrição:</strong> {lembrete.Descricao}</p>
                <p>Seu compromisso inicia-se em {paramGeral.QuantidadeDateTimeAntesDoInicio} {unidade}.</p>
                """,
                IsHtml = true
            };
        }

        private static string ObterUnidadeTexto(EnumUnidadeTempo unidade)
        {
            return unidade switch
            {
                EnumUnidadeTempo.Dias => "dia(s)",
                EnumUnidadeTempo.Horas => "hora(s)",
                EnumUnidadeTempo.Minutos => "minuto(s)",
                _ => "tempo"
            };
        }
    }
}

