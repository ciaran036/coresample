using System;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Repositories;
using Entities.Interfaces;

namespace Services
{
    public class Service : IService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUnitOfWork _unitOfWork;

        public Service(IServiceProvider serviceProvider, IUnitOfWork unitOfWork)
        {
            _serviceProvider = serviceProvider;
            _unitOfWork = unitOfWork;
        }

        public IQueryable<T> GetAll<T>() where T : class, IEntityBase, IActivatableEntity, new()
        {
            var repository = GetRepository<T>();
            return repository.GetAll.Where(x => x.Active);
        }

        public T Get<T>(int id) where T : class, IEntityBase, IActivatableEntity, new()
        {
            return GetAll<T>().FirstOrDefault(x => x.Id == id);
        }

        public async Task<T> Create<T>(T entity) where T : class, IEntityBase, IActivatableEntity, new()
        {
            var repository = GetRepository<T>();
            var newItem = await repository.Create(entity);
            _unitOfWork.Save();
            return newItem;
        }

        public void Deactivate<T>(int id) where T : class, IEntityBase, IActivatableEntity, new()
        {
            var repository = GetRepository<T>();
            var record = Get<T>(id);
            repository.Deactivate(record);
            _unitOfWork.Save();
        }

        public void Edit<T>(T entity) where T : class, IEntityBase, IActivatableEntity, new()
        {
            var repository = GetRepository<T>();
            repository.Edit(entity);
            _unitOfWork.Save();
        }

        private IGenericRepository<T> GetRepository<T>() where T : class, IEntityBase, IActivatableEntity, new()
        {
            return _serviceProvider.GetService(typeof(IGenericRepository<T>)) as IGenericRepository<T>;
        }
    }
}