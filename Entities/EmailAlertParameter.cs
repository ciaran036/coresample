using Entities.Interfaces;

namespace Entities
{
    public class EmailAlertParameter : IEntityBase
    {
        public int Id { get; set; }
        public int EmailAlertId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}
