using System.Linq;
using System.Threading.Tasks;
using Entities.Interfaces;

namespace Services
{
    public interface IService
    {
        IQueryable<T> GetAll<T>()
            where T : class, IEntityBase, IActivatableEntity, new();

        T Get<T>(int id) where T : class, IEntityBase, IActivatableEntity, new();
        Task<T> Create<T>(T entity) where T : class, IEntityBase, IActivatableEntity, new();
        void Deactivate<T>(int id) where T : class, IEntityBase, IActivatableEntity, new();
        void Edit<T>(T entity) where T : class, IEntityBase, IActivatableEntity, new();
    }
}