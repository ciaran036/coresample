using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Enums;
using Entities.Interfaces;

namespace Entities
{
    public class Error : IEntityBase
    {
        public Error()
        {
            this.DateCreated = DateTime.Now;
            this.Status = ErrorStatus.Pending;
        }

        public int Id { get; set; }
        public string Message { get; set; }

        [DisplayName("Inner Exception")]
        public string InnerException { get; set; }

        public string Url { get; set; }

        [DisplayName("Stack Trace")]
        public string StackTrace { get; set; }

        [DisplayName("Date Created")]
        public DateTime DateCreated { get; set; }

        [DisplayName("Created By User")]
        public int? CreatedByUserId { get; set; }

        public ErrorStatus Status { get; set; }

        [ForeignKey("CreatedByUserId")]
        public virtual User CreatedByUser { get; set; }
    }
}
