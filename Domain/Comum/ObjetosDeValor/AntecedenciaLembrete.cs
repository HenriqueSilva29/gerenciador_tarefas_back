using Domain.Entidades;

namespace Domain.Comum.ObjetosDeValor
{
    public readonly struct AntecedenciaLembrete
    {
        public TimeSpan Value { get; }

        private AntecedenciaLembrete(TimeSpan value)
        {
            if (value <= TimeSpan.Zero)
                throw new ArgumentException("A antecedência deve ser maior que zero.");

            Value = value;
        }

        public static AntecedenciaLembrete From(int quantidade, EnumUnidadeTempo unidade)
        {
            var timeSpan = unidade switch
            {
                EnumUnidadeTempo.Minutos => TimeSpan.FromMinutes(quantidade),
                EnumUnidadeTempo.Horas => TimeSpan.FromHours(quantidade),
                EnumUnidadeTempo.Dias => TimeSpan.FromDays(quantidade),
                _ => throw new ArgumentException("Unidade inválida")
            };

            return new AntecedenciaLembrete(timeSpan);
        }
    }
}

