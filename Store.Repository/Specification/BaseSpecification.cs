﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {

        public BaseSpecification(Expression<Func<T, bool>> criteria) 
        {
            Criteria = criteria;    
        }
        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public int Take   { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
            => Includes.Add(includeExpression);

        protected void AddOrderBY(Expression<Func<T, object>> orderByExpression)
            => OrderBy = orderByExpression;

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByExpressionDescending)
            => OrderByDescending = orderByExpressionDescending;
    
        protected void ApplayPagination(int skip, int take)
        {
            Take = take;
            Skip = skip;
            IsPaginated = true;
        }
    
    }
}
