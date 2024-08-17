using Store.Data.Context;
using Store.Data.Entites;
using Store.Reposatry.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Reposatry.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext context;
        private Hashtable _repositores;
        public UnitOfWork(StoreDbContext context) 
        {
            this.context = context;
        }
        public async Task<int> CopleteAsync()
            => await context.SaveChangesAsync();

        public IGenericRepositpry<TEntity, TKey> Repositpry<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            if (_repositores is null)
            {
                _repositores = new Hashtable();
            }
            var entityKey=typeof(TEntity).Name;
            if (!_repositores.ContainsKey(entityKey))
            {
                var repositoryType = typeof(GenericRepositpry<,>);
                var repositoryInstance= Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity),typeof(TKey)),context);
                _repositores.Add(entityKey, repositoryInstance);
            }
            return( IGenericRepositpry<TEntity, TKey >) _repositores[entityKey];

        }
    }
}
