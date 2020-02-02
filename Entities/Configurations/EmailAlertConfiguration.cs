namespace Entities.Configurations
{
    public class EmailAlertConfiguration : EntityBaseConfiguration<EmailAlert>
    {
        public EmailAlertConfiguration()
        {
            Property(x => x.Description).HasMaxLength(100);
            Property(x => x.Subject).HasMaxLength(255);
        }
    }
}
