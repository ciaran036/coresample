using System;
using System.ComponentModel.DataAnnotations;
using Common;
using Entities.Interfaces;

namespace Entities
{
    /// <summary>
    /// When and who created and modified a record
    /// </summary>
    public class AuditedEntity : IAuditedEntity
    {
        /// <summary>
        /// User that created the record
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Date the record was created
        /// </summary>
        [DisplayFormat(DataFormatString = Constants.DateFormatString, ApplyFormatInEditMode = true)]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Most recent user to modify the record
        /// </summary>
        public string LastModifiedBy { get; set; }

        /// <summary>
        /// Date of most recent edit to the record
        /// </summary>
        [DisplayFormat(DataFormatString = Constants.DateFormatString, ApplyFormatInEditMode = true)]
        public DateTime? LastModifiedDate { get; set; }
    }
}
