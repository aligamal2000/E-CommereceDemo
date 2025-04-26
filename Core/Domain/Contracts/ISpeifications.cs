﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface ISpeifications<TEntity,Tkey> where TEntity : ModelBase<Tkey>
    {
         Expression<Func<TEntity, bool>>? Criteria {  get; } 
         List<Expression<Func<TEntity,object>>> IncludeExpressions {  get; }    
        Expression<Func<TEntity,object>> OrderBy   { get; }
        Expression<Func<TEntity, object>> OrderByDesc { get; }
        public int Take { get; }
        public int Skip { get; }
        public bool IsPaginated { get; }
    }
}
