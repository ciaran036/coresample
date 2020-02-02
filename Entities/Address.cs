using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Entities.Interfaces;

namespace Entities
{
    //[Validator(typeof(AddressValidation))]
    public class Address : AddressBase, IEntityBase
    {
        public int Id { get; set; }

        [ForeignKey(nameof(CountyId))]
        public virtual County County { get; set; }

        [ForeignKey(nameof(CountryId))]
        public virtual Lookup Country { get; set; }
    }
}