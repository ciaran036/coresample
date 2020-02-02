using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Entities.Interfaces;

namespace Entities
{
    public class AddressBase : IAddressBase
    {
        [Display(Name = "Street")]
        [Description("Enter a street and house number")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Town / City")]
        [Description("Enter a town / city")]
        public string AddressLine2 { get; set; }

        [Display(Name = "County")]
        [Description("Select a county")]
        public int? CountyId { get; set; }

        [Display(Name = "Country")]
        [Description("Select a country")]
        public int? CountryId { get; set; }

        [Display(Name = "Postcode")]
        [Description("Select a valid postcode")]
        public string Postcode { get; set; }

        public bool IsNull()
        {
            return string.IsNullOrEmpty(AddressLine1) &&
                   string.IsNullOrEmpty(AddressLine2) &&
                   !CountyId.HasValue &&
                   !CountryId.HasValue &&
                   string.IsNullOrEmpty(Postcode);
        }
    }
}