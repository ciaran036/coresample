using System.ComponentModel;
using Entities.Interfaces;

namespace Entities
{
    public class Lookup : LookupBase, IEntityBase
    {
        [DisplayName("Type")]
        public int LookupTypeId { get; set; }

        public virtual LookupType LookupType { get; set; }

        [Description("Select a colour for use on dashboard / calendar")]
        public string Colour { get; set; } = string.Empty;

        [Description("Select an icon to represent the category")]
        public string Icon { get; set; }

        [DisplayName("Icon Unicode")]
        public string IconUnicode { get; set; }
    }
}
