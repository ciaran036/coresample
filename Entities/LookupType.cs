using System.ComponentModel;
using Entities.Enums;
using Entities.Interfaces;

namespace Entities
{
    public class LookupType : IEntityBase
    {
        public int Id { get; set; }
        [Description("Enter the name of the lookup")]
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public LookupTypeCode Code { get; set; }
        public int SortOrder { get; set; }
        public bool Active { get; set; }

        public LookupType()
        {
            Active = true;
        }
    }
}
