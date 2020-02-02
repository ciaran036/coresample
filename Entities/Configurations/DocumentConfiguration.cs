namespace Entities.Configurations
{
    public class DocumentConfiguration : EntityBaseConfiguration<Document>
    {
        public DocumentConfiguration()
        {
            Property(x => x.Name).HasMaxLength(150);
            Property(x => x.DocumentTypeId).IsRequired();
            Property(x => x.UserId).IsRequired();
        }
    }
}
