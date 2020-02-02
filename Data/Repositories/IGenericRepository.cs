using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Interfaces;

namespace Data.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntityBase, new()
    {
        IQueryable<TEntity> GetAll { get; }
        Task<TEntity> Get(int id);
        Task<TEntity> Create(TEntity entity);
        Task Create(IEnumerable<TEntity> entities);
        Task Delete(int id);
        void Delete(IEnumerable<TEntity> entities);
        TEntity Edit(TEntity entity);

        void Deactivate<TActivatableEntity>(TActivatableEntity entity)
            where TActivatableEntity : IActivatableEntity;
    }
}