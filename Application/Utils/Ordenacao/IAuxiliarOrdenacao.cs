using System.Linq.Expressions;

namespace Application.Utils.Ordenacao
{
    public interface IAuxiliarOrdenacao<T>
    {
        string OrdenarPor { get; set; }
        string Direcao { get; set; }

        Dictionary<string, Expression<Func<T, object>>> ObterCamposOrdenaveis();
    }
}

