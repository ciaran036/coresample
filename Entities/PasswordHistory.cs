using System;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Interfaces;

namespace Entities
{
    public class PasswordHistory : IEntityBase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Password { get; set; }
        public DateTime DateAdded { get; set; }
        public bool Active { get; set; }
        public string Key { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public PasswordHistory()
        {
            this.Active = true;
            this.DateAdded = DateTime.Now;
        }
    }
}
