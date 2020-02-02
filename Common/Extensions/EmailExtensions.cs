using System.Net.Mail;

namespace Common.Extensions
{
    public static class EmailExtensions
    {
        public static SmtpDeliveryMethod MapDeliveryMethod(this string deliveryMethod)
        {
            switch (deliveryMethod)
            {
                case "PickupDirectoryFromIis": return SmtpDeliveryMethod.PickupDirectoryFromIis;
                case "SpecifiedPickupDirectory": return SmtpDeliveryMethod.SpecifiedPickupDirectory;
                default:
                case "Network":
                    return SmtpDeliveryMethod.Network;
            }
        }
    }
}
