using PayService.Core.Exception;

namespace PayService.Core.ValueObject
{
    public class Month
    {
        private readonly string _month;
        private readonly string[] _months;

        public Month(string month)
        {
            _months = new string[] { 
                "01", // January
                "02", // Febuary
                "03", // March
                "04", // April
                "05", // May
                "06", // June
                "07", // July
                "08", // August
                "09", // September
                "10", // October
                "11", // November
                "12"  // December
            };
            ValidateParameters(month);
            _month = FormatMonth(month);
        }

        private void ValidateParameters(string month)
        {
            if (string.IsNullOrEmpty(month))
            {
                throw new DomainException("You must inform a month number (MM)!");
            }

            var auxMonth = FormatMonth(month);

            if (auxMonth.Length != 2)
            {
                throw new DomainException("Invalid month format!");
            }

            if (!_months.Contains(auxMonth))
            {
                throw new DomainException($" The {auxMonth} is an invalid month!");
            }
        }

        private static string FormatMonth(string month)
        {
            if (month.Length == 1)
            {
                return $"0{month}";
            }

            return month;
        }

        public override string ToString()
        {
            return _month;
        }
    }
}
