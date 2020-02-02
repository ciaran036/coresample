using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Entities
{
    public class User : IdentityUser, IAuditedEntity, IAddressBase
    {
        public User()
        {
            Active = true;
            IsEcomAccount = false;
        }

        public const string EcomAdmin = "ecomadmin";

        public int? TitleId { get; set; }

        public string Forenames { get; set; }

        public string Surname { get; set; }

        public string ContactNumber { get; set; }

        public bool Active { get; set; }

        public bool IsEcomAccount { get; set; }

        [NotMapped]
        public string FullName => $"{Forenames} {Surname}";

        public DateTime? PasswordValidToDate { get; set; }

        public bool PasswordExpired { get; set; }

        //[NotMapped]
        //public bool IsAdministrator =>
        //    Roles.Select(role => role.Name).Any(role => role.IsAnyOf(Common.Roles.SuperAdmin, Common.Roles.Admin));

        [ForeignKey(nameof(TitleId))]
        public virtual Lookup Title { get; set; }

        [ForeignKey(nameof(CountryId))]
        public virtual Lookup Country { get; set; }

        [ForeignKey(nameof(CountyId))]
        public virtual County County { get; set; }

        /// <summary>
        /// Navigation property for the roles this user belongs to.
        /// </summary>
        public virtual ICollection<IdentityUserRole<string>> Roles { get; } = new List<IdentityUserRole<string>>();

        /// <summary>
        /// Navigation property for the claims this user possesses.
        /// </summary>
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; } = new List<IdentityUserClaim<string>>();

        /// <summary>
        /// Navigation property for this users login accounts.
        /// </summary>
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; } = new List<IdentityUserLogin<string>>();

        // TODO: What is .NET Core equivalent of below?
        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        //{
        //    // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
        //    // Add custom user claims here
        //    return userIdentity;
        //}

        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int? CountyId { get; set; }
        public int? CountryId { get; set; }
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