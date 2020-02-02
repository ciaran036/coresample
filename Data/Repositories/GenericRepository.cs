using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntityBase, new()
    {
        private readonly DataContext _dbContext;

        public GenericRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetAll => _dbContext.Set<TEntity>().AsNoTracking();

        public Task<TEntity> Get(int id)
        {
            return _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            var newItem = await _dbContext.Set<TEntity>().AddAsync(entity);
            return newItem.Entity;
        }

        public async Task Create(IEnumerable<TEntity> entities)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
        }

        public void Deactivate<TActivatableEntity>(TActivatableEntity entity) 
            where TActivatableEntity : IActivatableEntity
        {
            var dbEntityEntry = _dbContext.Entry(entity);
            entity.Active = false;
            dbEntityEntry.State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var entity = await Get(id);
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }

        public TEntity Edit(TEntity entity)
        {
            var updatedEntity = _dbContext.Set<TEntity>().Update(entity);
            return updatedEntity.Entity;
        }
    }
}