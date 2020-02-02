
using System.ComponentModel;
using Entities.Attributes;

namespace Entities.Enums
{
    public enum JobLogStatus
    {
        Error,

        Warning,

        [Description("In progress")]
        Info,

        // Job completion statuses

        [JobCompletionStatus]
        [Description("Completed without error")]
        Completed,

        [JobCompletionStatus]
        [Description("Completed with warnings. Please review log.")]
        CompletedWithWarnings,

        [JobCompletionStatus]
        [Description("Completed with errors")]
        CompletedWithErrors,

        [JobCompletionStatus]
        [Description("Fatal error. Job terminated before completion")]
        Fatal,

        [JobCompletionStatus]
        [Description("External failure. An external system failed")]
        ExternalFailure
    }
}
