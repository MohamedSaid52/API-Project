﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace API.BLL.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T>
    {
        public BaseSpecifications(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Include { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagining { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> include)
        {
            Include.Add(include);
        }

        protected void AddOrderBy(Expression<Func<T,object>> orderby)
        {
            OrderBy = orderby;
        }

        protected void AddOrderByDescinding(Expression<Func<T, object>> orderbydescinding)
        {
            OrderByDescending = orderbydescinding;
        }

        protected void ApplyPaging(int skip,int take)
        {
            Take = take;
            Skip = skip;
            IsPagining=true;
        }
    }
}
