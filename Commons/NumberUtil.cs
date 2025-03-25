using System.Globalization;

namespace G5_MovieTicketBookingSystem.Commons
{
    public class NumberUtil
    {
        public static string FormatCurrency(decimal amount)
        {
            var culture = new CultureInfo("vi-VN");
            return string.Format(culture, "{0:C0}", amount); // C0 = currency, 0 decimal places
        }
    }
}
