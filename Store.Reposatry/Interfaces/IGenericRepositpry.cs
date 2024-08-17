using Store.Data.Entites;
using Store.Reposatry.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Reposatry.Interfaces
{
    public interface IGenericRepositpry<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> GetByIdAsync(TKey? id);

        Task<IReadOnlyList<TEntity>> GetByAllAsync();


        Task<TEntity> GetWithSpecificationByIdAsync(ISpecification<TEntity> spesc );

        Task<IReadOnlyList<TEntity>> GetByAllWithSpecificationAsync(ISpecification<TEntity> spesc);
        Task<int> CountSpecificationAsync(ISpecification<TEntity> spesc);
        Task AddAsync(TEntity entity);
        void Updat(TEntity entity);
        void Delete(TEntity entity);

    }
}
