using System.ComponentModel;

namespace Entities.Enums
{
    public enum AuditAction
    {
        [Description("Creation")]
        Creation = 1,

        [Description("Modification")]
        Modification = 2,

        [Description("Deletion")]
        Deletion = 3,

        [Description("Completion")]
        Completion = 6,
    }
}