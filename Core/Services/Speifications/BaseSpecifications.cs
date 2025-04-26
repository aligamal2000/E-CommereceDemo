using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;

namespace Services.Speifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpeifications<TEntity, TKey>
      where TEntity : ModelBase<TKey>
    {
        #region Criteria
        public BaseSpecifications(Expression<Func<TEntity, bool>>? PassedExpression)
        {
            Criteria = PassedExpression;
        }

        public Expression<Func<TEntity, bool>>? Criteria { get; private set; } 
        #endregion

        #region Includes
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new List<Expression<Func<TEntity, object>>>();



        protected void AddInclude(Expression<Func<TEntity, object>> IncludeExp)
        {
            IncludeExpressions.Add(IncludeExp);
        }
        #endregion
        #region Sorting
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDesc {  get; private set; }

       

        protected void AddOrdetBy(Expression<Func<TEntity,object>> OrderByExpression)=> OrderBy = OrderByExpression;
        protected void AddOrdetByDesc(Expression<Func<TEntity, object>> OrderByDescExpression) => OrderBy = OrderByDescExpression;

        #endregion
        #region Pagination
        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get;  set; }
        public void ApplyPagination(int PageSize, int PageIndex)
        {
            Skip = (PageIndex-1)*PageSize;
            Take = PageSize;
            IsPaginated = true;
        }
        #endregion
    }

}
