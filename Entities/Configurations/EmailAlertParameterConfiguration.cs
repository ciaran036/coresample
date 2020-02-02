namespace Entities.Configurations
{
    public class EmailAlertParameterConfiguration : EntityBaseConfiguration<EmailAlertParameter>
    {
        public EmailAlertParameterConfiguration()
        {
            Property(x => x.Name).HasMaxLength(50);
            Property(x => x.Description).HasMaxLength(255);
        }
    }
}
