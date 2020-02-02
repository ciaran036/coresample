using Entities.Interfaces;

namespace Entities
{
    public class EmailTemplate : AuditedEntity, IEntityBase, IActivatableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool Active { get; set; }
    }
}