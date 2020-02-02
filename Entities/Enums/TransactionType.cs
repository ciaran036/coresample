using System.ComponentModel;

namespace Entities.Enums
{
    public enum TransactionType
    {
        [Description("User Wizard")]
        User = 10,

        [Description("Example Wizard")]
        ExampleWizard = 20
    }
}
