namespace Entities.Configurations
{
    public class UserConfiguration : EntityBaseConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(u => u.Username).IsRequired().HasMaxLength(200);
            Property(u => u.Email).IsRequired().HasMaxLength(200);
            Property(x => x.AddressLine1).HasMaxLength(255);
            Property(x => x.AddressLine2).HasMaxLength(100);
            Property(x => x.Postcode).HasMaxLength(10);
            Property(x => x.ContactNumber).HasMaxLength(50);
            Property(u => u.Forename).HasMaxLength(255);
            Property(u => u.Surname).HasMaxLength(255);
            Property(u => u.IsLocked).IsRequired();
            Property(u => u.DateCreated);
        }
    }
}
