using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Enums;
using Entities.Interfaces;

namespace Entities
{
    public class UserActionAudit : IEntityBase
    {
        public int Id { get; set; }

        [Display(Name = "Date Occurred")]
        public DateTime DateOccurred { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Entity Type")]
        public string EntityType { get; set; }

        public AuditAction Action { get; set; }

        public string ActionName { get; set; }

        public int? EntityId { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public UserActionAudit()
        {
            Active = true;
            DateOccurred = DateTime.Now;
        }
    }
}
