using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class ExtendedPropertyDropdownOption
    {
        public int Id { get; set; }
        public int ExtendedPropertyDropdownId { get; set; }
        public string DisplayText { get; set; }
        public string Value { get; set; }
        public int SortOrder { get; set; }
        public bool Active { get; set; }

        [ForeignKey("ExtendedPropertyDropdownId")]
        public virtual ExtendedPropertyDropdown ExtendedPropertyDropdown { get; set; }
    }
}
