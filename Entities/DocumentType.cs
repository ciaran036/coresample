using System.ComponentModel;
using Entities.Interfaces;

namespace Entities
{
    public class DocumentType : IEntityBase, ILookupBase
    {
        public int Id { get; set; }
        [Description("Enter a name for the new document type.")]
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public int Code { get; set; }
        public bool Active { get; set; }

        public DocumentType()
        {
            this.Active = true;
            this.SortOrder = 0;
        }
    }
}
