namespace Entities.Configurations
{
    public class RoleConfiguration : EntityBaseConfiguration<Role>
    {
        public RoleConfiguration()
        {
            Property(u => u.Id).IsRequired();
            Property(u => u.Name).IsRequired();
        }
    }
}
