namespace Data
{
    public interface IUnitOfWork
    {
        void Save(string auditMessage = null, bool enableUserAudit = true);
    }
}