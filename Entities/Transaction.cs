using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Enums;
using Entities.Interfaces;

namespace Entities
{
    public class Transaction : IGuidEntityBase
    {
        public Transaction()
        {
            this.Active = true;
            this.DateCreated = DateTime.Now;
            this.Status = TransactionStatus.Open;
        }

        public Guid Id { get; set; }

        [DisplayName("Value")]
        public string JsonValue { get; set; }
        public TransactionType Type { get; set; }
        public TransactionStatus Status { get; set; }

        [DisplayName("Created By")]
        public int CreatedById { get; set; }

        [DisplayName("Date Created")]
        public DateTime DateCreated { get; set; }

        public bool Active { get; set; }

        [ForeignKey(nameof(CreatedById))]
        public virtual User CreatedBy { get; set; }
    }
}
