using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entites;
using Store.Reposatry.Interfaces;
using Store.Reposatry.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Reposatry.Repositories
{
    public class GenericRepositpry<TEntity, TKey> : IGenericRepositpry<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext context;

        public GenericRepositpry(StoreDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
        }

        public async Task<int> CountSpecificationAsync(ISpecification<TEntity> spesc)
        {
           return await SpecificationEvaluater<TEntity, TKey>.GetQuery(context.Set<TEntity>(), spesc).CountAsync();
        }

        public void Delete(TEntity entity)
        {
           context.Set<TEntity>().Remove(entity);
        }

        public async Task<IReadOnlyList<TEntity>> GetByAllAsync()
        {
          return  await context.Set<TEntity>().ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetByAllWithSpecificationAsync(ISpecification<TEntity> spesc)
        {
          return  await SpecificationEvaluater<TEntity, TKey>.GetQuery(context.Set<TEntity>(), spesc).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey? id)
        {
            return await context.Set<TEntity>().FindAsync(id);

        }

        public async Task<TEntity> GetWithSpecificationByIdAsync(ISpecification<TEntity> spesc)
        {
            return await SpecificationEvaluater<TEntity, TKey>.GetQuery(context.Set<TEntity>(), spesc).FirstOrDefaultAsync();

        }

        public void Updat(TEntity entity)
        {
           context.Set<TEntity>().Update(entity);

        }
    }
}
