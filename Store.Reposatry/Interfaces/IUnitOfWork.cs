using Store.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Reposatry.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepositpry<TEntity,TKey> Repositpry<TEntity, TKey>() where TEntity : BaseEntity<TKey>;

        Task <int> CopleteAsync();
    }
}
