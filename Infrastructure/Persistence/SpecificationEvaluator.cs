using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public static class SpecificationEvaluator
    {
        public  static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> InputQuery, ISpeifications<TEntity, TKey> Spec)
     where TEntity : ModelBase<TKey>
        {
            var Query = InputQuery;

            if (Spec.Criteria is not null)
                Query = Query.Where(Spec.Criteria);
            
            if (Spec.OrderBy is not null)
                Query = Query.OrderBy(Spec.OrderBy);
            if(Spec.OrderByDesc is not null)
                Query = Query.OrderByDescending(Spec.OrderByDesc);


            if (Spec.IncludeExpressions is not null && Spec.IncludeExpressions.Count > 0)
                Query = Spec.IncludeExpressions.Aggregate(Query, (currentQuery, Exp) => currentQuery.Include(Exp));

            if (Spec.IsPaginated==true)
            {
                Query = Query.Skip(Spec.Skip).Take(Spec.Take);
            }
            return Query;
        }
    }
}
