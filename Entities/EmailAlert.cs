using System.Collections.Generic;
using Entities.Interfaces;

namespace Entities
{
    public class EmailAlert : IEntityBase
    {
        public EmailAlert()
        {
            this.EmailAlertParameter = new HashSet<EmailAlertParameter>();
        }

        public int Id { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }
        public string MailText { get; set; }
        public bool Enabled { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<EmailAlertParameter> EmailAlertParameter { get; set; }
    }
}
