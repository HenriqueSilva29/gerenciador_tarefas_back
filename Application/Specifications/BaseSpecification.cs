using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications
{
    public abstract class BaseSpecification<T> : IBaseSpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; protected set; }
        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public BaseSpecification<T> And(BaseSpecification<T> other)
        {
            var param = Expression.Parameter(typeof(T));
            var body = Expression.AndAlso(
                Expression.Invoke(this.Criteria, param),
                Expression.Invoke(other.Criteria, param)
            );
            return new DirectSpecification<T>(Expression.Lambda<Func<T, bool>>(body, param));
        }

        public BaseSpecification<T> Or(BaseSpecification<T> other)
        {
            var param = Expression.Parameter(typeof(T));
            var body = Expression.OrElse(
                Expression.Invoke(this.Criteria, param),
                Expression.Invoke(other.Criteria, param)
            );
            return new DirectSpecification<T>(Expression.Lambda<Func<T, bool>>(body, param));
        }

        public class DirectSpecification<T> : BaseSpecification<T>
        {
            public DirectSpecification(Expression<Func<T, bool>> criteria) : base(criteria) { }
        }
    }
}
