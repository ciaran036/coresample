using System.ComponentModel;
using Entities.Interfaces;

namespace Entities
{
    public class LookupBase : ILookupBase, IActivatableEntity
    {
        public LookupBase()
        {
            Active = true;
        }

        public int Id { get; set; }
        [Description("Enter a name.")]
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public bool Active { get; set; }
    }
}
