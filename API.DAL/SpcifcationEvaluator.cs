using API.BLL.Entities;
using API.BLL.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DAL
{
    public class SpcifcationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> InputQuery, ISpecifications<TEntity> specifications)
        {
            var query = InputQuery;
            if(specifications.Criteria!=null)
                query=query.Where(specifications.Criteria);
            if (specifications.OrderBy!=null)
                query = query.OrderBy(specifications.OrderBy);
            if (specifications.OrderByDescending != null)
                query = query.OrderByDescending(specifications.OrderByDescending);
            if(specifications.IsPagining)
                query=query.Skip(specifications.Skip).Take(specifications.Take);
            query = specifications.Include.Aggregate(query, (current, include) => current.Include(include));
            return query;
        }
    }
}
