using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.DAL.Repository.Contract
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity, CancellationToken cancellationToken=default);

        Task<bool> Delete(Expression<Func<TEntity, bool>> Predicate, CancellationToken cancellationToken=default);
        Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> Predicate, TEntity entity, CancellationToken cancellationToke=default);
        Task<TEntity?> GetItemAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToke = default, string? Selector = null);

        Task<bool> IsExit(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToke = default);


        //REad------------------------
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToke = default,string? Selector = null);
        Task<IEnumerable<TEntity>> GetAllWithFilter(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToke = default);
        Task<IEnumerable<TEntity>> ReadAllAsync( CancellationToken cancellationToke = default, string? Selector = null);
    }

}
