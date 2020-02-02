namespace Entities.Configurations
{
    public class UserActionAuditConfiguration : EntityBaseConfiguration<UserActionAudit>
    {
        public UserActionAuditConfiguration()
        {
            Property(u => u.DateOccurred).IsRequired();
            Property(u => u.UserId).IsRequired();
            Property(u => u.DeputiseId).IsOptional();
            Property(u => u.EntityType).IsRequired();
            Property(u => u.Active).IsRequired();
        }
    }
}
