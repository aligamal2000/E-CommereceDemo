using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDBContext context) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _Repositories = new Dictionary<string, object>();

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : ModelBase<TKey>
        {
            //TypeName
            var TypeName = typeof(TEntity).Name;

            if (_Repositories.ContainsKey(TypeName))
                return (IGenericRepository<TEntity, TKey>)_Repositories[TypeName];

            var Repo = new GenericRepository<TEntity, TKey>(context);

            _Repositories.Add(TypeName, Repo);

            return Repo;
        }

        public async Task<int> SaveChangeAsync()
        {
            return await context.SaveChangesAsync();
        }
    }

}
