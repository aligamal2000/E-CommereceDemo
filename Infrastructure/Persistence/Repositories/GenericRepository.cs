using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(StoreDBContext context) : IGenericRepository<TEntity, TKey>
      where TEntity : ModelBase<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetByIdAsync(TKey id)
            => await context.Set<TEntity>().FindAsync(id);

        public void Add(TEntity entity)
            => context.Set<TEntity>().Add(entity);

        public void Update(TEntity entity)
 => context.Set<TEntity>().Update(entity);
        public void Delete(TEntity entity)
          => context.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpeifications<TEntity, TKey> Spec)
        {
            return await SpecificationEvaluator.CreateQuery(context.Set<TEntity>(), Spec).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(ISpeifications<TEntity, TKey> Spec)
        {
            return await SpecificationEvaluator.CreateQuery(context.Set<TEntity>(), Spec).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpeifications<TEntity, TKey> Spec)
       => await SpecificationEvaluator.CreateQuery(context.Set<TEntity>(), Spec).CountAsync();
    }


}
