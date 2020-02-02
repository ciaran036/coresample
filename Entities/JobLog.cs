using System;
using System.ComponentModel.DataAnnotations;
using Entities.Enums;
using Entities.Interfaces;

namespace Entities
{
    public class JobLog : IEntityBase
    {
        public int Id { get; set; }

        [Display(Name="Job Id")]
        public Guid JobId { get; set; }

        [Display(Name="Job Name")]
        public string JobName { get; set; }

        [Display(Name="Result")]
        public JobLogStatus JobLogStatus { get; set; }

        public string Message { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime Timestamp { get; set; }

        public string Exception { get; set; }

        public string InnerException { get; set; }

        public string StackTrace { get; set; }
    }
}
