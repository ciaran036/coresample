namespace Entities.Configurations
{
    public class SettingsConfiguration : EntityBaseConfiguration<Settings>
    {
        public SettingsConfiguration()
        {
            Property(x => x.Name).IsRequired();
            Property(x => x.DataValueTypeId).IsRequired();
            Property(x => x.SortOrder).IsOptional();
            Property(x => x.Active).IsRequired();
        }
    }
}
