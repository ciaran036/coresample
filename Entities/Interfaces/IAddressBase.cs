using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities.Interfaces
{
    public interface IAddressBase
    {
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        int? CountyId { get; set; }
        int? CountryId { get; set; }
        string Postcode { get; set; }
        bool IsNull();
    }
}