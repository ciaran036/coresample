using System.Collections.Generic;
using Entities.Interfaces;

namespace Entities
{
    public class ExtendedPropertyDropdown : IEntityBase
    {
        public ExtendedPropertyDropdown()
        {
            this.ExtendedPropertyDropdownOption = new HashSet<ExtendedPropertyDropdownOption>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayText { get; set; }
        public int SortOrder { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<ExtendedPropertyDropdownOption> ExtendedPropertyDropdownOption { get; set; }
    }
}
