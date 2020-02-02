using Entities.Interfaces;

namespace Entities
{
    public class ExtendedPropertyType : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public int SortOrder { get; set; }
        public bool Active { get; set; }
    }
}
