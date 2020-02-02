using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;

        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Save(string auditMessage, bool enableUserAudit)
        {
            _dataContext.SaveChanges();
        }
    }
}