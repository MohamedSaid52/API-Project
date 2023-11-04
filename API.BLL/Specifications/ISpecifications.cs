using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace API.BLL.Specifications
{
    public interface ISpecifications<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Include { get;}
        Expression<Func<T,Object>> OrderBy { get; }
        Expression<Func<T, Object>> OrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagining { get; }

    }
}
