using System;

namespace Entities.Interfaces
{
    public interface IAuditedEntity
    {
        /// <summary>
        /// User that created the record
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// Date the record was created
        /// </summary>
        DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Most recent user to modify the record
        /// </summary>
        string LastModifiedBy { get; set; }

        /// <summary>
        /// Date of most recent edit to the record
        /// </summary>
        DateTime? LastModifiedDate { get; set; }
    }
}