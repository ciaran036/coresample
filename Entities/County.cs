using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Interfaces;

namespace Entities
{
    public class County : LookupBase, IEntityBase
    {
        [Display(Name="Country")]
        [Description("Select a country.")]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Lookup Country { get; set; }
    }
}
