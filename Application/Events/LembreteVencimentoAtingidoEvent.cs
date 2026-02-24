public record LembreteVencimentoAtingidoEvent
{
    public int LembreteId { get; init; }

    public LembreteVencimentoAtingidoEvent(int lembreteId)
    {
        LembreteId = lembreteId;
    }
}