using Domain.Entities;

namespace Domain.Common.ValueObjects
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

        public static AntecedenciaLembrete From(int quantidade, UnidadeTempo unidade)
        {
            var timeSpan = unidade switch
            {
                UnidadeTempo.Minutos => TimeSpan.FromMinutes(quantidade),
                UnidadeTempo.Horas => TimeSpan.FromHours(quantidade),
                UnidadeTempo.Dias => TimeSpan.FromDays(quantidade),
                _ => throw new ArgumentException("Unidade inválida")
            };

            return new AntecedenciaLembrete(timeSpan);
        }
    }
}
