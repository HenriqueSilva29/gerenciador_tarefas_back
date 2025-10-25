using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Infra.Utils.SortHelperUtils
{
    public static class SortHelperUtil<T>
    {
        public static IQueryable<T> ExecutarOrdenacao(IQueryable<T> query, string colunaOrdenada, string ascDescOrdenada)
        {
            var propertyInfo = typeof(T).GetProperty(colunaOrdenada);

            if (propertyInfo == null)
            {
                propertyInfo = typeof(T).GetProperties().FirstOrDefault(p => p.Name.Contains("codigo", StringComparison.OrdinalIgnoreCase));
            
                if (propertyInfo == null)
                return query;
            }

            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyExpression = Expression.Property(parameter, propertyInfo);
            var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(propertyExpression, typeof(object)), parameter);

            var method = ascDescOrdenada.ToLower() == "asc" ? "OrderBy" : "OrderByDescending";

            var resultExpression = Expression.Call(
                typeof(Queryable),
                method,
                new Type[] { typeof(T), typeof(object) },
                query.Expression,
                lambda
            );

            return query.Provider.CreateQuery<T>(resultExpression);
        }

    }

    public enum EnumOrdenacao
    {
        [Description("asc")]
        Ascendente,
        [Description("desc")]
        Descendente
    }
}