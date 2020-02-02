using System;
using Entities.Interfaces;

namespace Entities
{
    public class LicenseNotification : IEntityBase
    {
        public int Id { get; set; }
        public DateTime NotificationDate { get; set; }
        public int FullLicenseCount { get; set; }
        public int ReadOnlyLicenseCount { get; set; }
        public int FullLicenseLimit { get; set; }
        public int ReadOnlyLicenseLimit { get; set; }
    }
}
