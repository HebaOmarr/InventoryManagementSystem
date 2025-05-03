using InventoryManagementSystem.DAL.DataContext;
using InventoryManagementSystem.DAL.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryManagementSystem.DAL.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDBContext _applicationDBContext;
        public GenericRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }
        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _applicationDBContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        }

        public async Task<bool> Delete(Expression<Func<TEntity, bool>> Predicate, CancellationToken cancellationToken = default)
        {
            TEntity? result = await _applicationDBContext.Set<TEntity>().FirstOrDefaultAsync(Predicate, cancellationToken);
            if (result is not null)
            {
                _applicationDBContext.Set<TEntity>().Remove(result);
                return true;
            }
            return false;
        }



        public async Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> Predicate, TEntity entity, CancellationToken cancellationToken = default)
        {
            TEntity? existedItem = await _applicationDBContext.Set<TEntity>().FirstOrDefaultAsync(Predicate, cancellationToken);
            if (existedItem is not null)
            {
                _applicationDBContext.Entry(existedItem).CurrentValues.SetValues(entity);
                _applicationDBContext.Entry(existedItem).State = EntityState.Modified;
                return true;
            }
            return false;
        }


        public async Task<TEntity?> GetItemAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default, string? Selector = null)
        {
            if (Selector is not null)
            {
                return await _applicationDBContext.Set<TEntity>().Include(Selector).FirstOrDefaultAsync(expression, cancellationToken);
            }
            return await _applicationDBContext.Set<TEntity>().FirstOrDefaultAsync(expression, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> ReadAllAsync(CancellationToken cancellationToken = default, string? Selector = null)
        {
            if (Selector is not null)
            {
                return await _applicationDBContext.Set<TEntity>().Include(Selector).AsNoTracking().ToListAsync(cancellationToken);
            }
            return await _applicationDBContext.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);
        }
           
        

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default, string? Selector = null)
        {
            if (Selector is not null)
            {
                return await _applicationDBContext.Set<TEntity>().Include(Selector).ToListAsync(cancellationToken);
            }
            return await _applicationDBContext.Set<TEntity>().ToListAsync(cancellationToken);
        }


        public async Task<IEnumerable<TEntity>> GetAllWithFilter(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _applicationDBContext.Set<TEntity>().Where(expression).ToListAsync(cancellationToken);
        }



        public async Task<bool> IsExit(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToke = default)
        {

            return await _applicationDBContext.Set<TEntity>().AnyAsync(expression,cancellationToke);


        }



    } 
}

