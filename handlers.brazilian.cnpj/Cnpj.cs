using System;
using System.Globalization;
using System.Linq;

namespace handlers.brazilian
{
    public static partial class Cnpj
    {
        public static bool IsValid(string cnpj)
        {
            bool returnValue = false;

            try
            {
                Check(cnpj);
                returnValue = true;
            }
            catch (Exception)
            {
                // return false for any error
                returnValue = false;
            }

            return returnValue;
        }

        public static void Check(string cnpj)
        {
            string digits = new string(cnpj.Where(char.IsDigit).ToArray());

            if (!Constants.CNPJ_LENGTH_WITHOUT_FORMATTING.Equals(digits.Length))
            {
                throw new FormatException(message: "Invalid argument length [" + digits.Length + "].");
            }

            if (HasRepeatedDigits(digits))
            {
                throw new ArgumentException(message: "Invalid CNPJ number.");
            }

            // verifies
            string verifiers = digits.Substring(digits.Length - 2, 2);
            // to be verified
            string cnpjNumbers = digits.Substring(0, 12);

            if (!verifiers[0].Equals(Mod11(cnpjNumbers)))
            {
                throw new ArgumentException(message: "Invalid CNPJ number.");
            }

            cnpjNumbers += verifiers[0];

            if (!verifiers[1].Equals(Mod11(cnpjNumbers)))
            {
                throw new ArgumentException(message: "Invalid CNPJ number.");
            }
        }

        public static string Format(string cnpj, bool check)
        {
            Check(cnpj);
            return Format(cnpj);
        }

        public static string Format(string cnpj)
        {
            if (cnpj == null)
            {
                throw new ArgumentNullException("Value cannot be null.");
            }

            if (string.Empty.Equals(cnpj))
            {
                throw new ArgumentOutOfRangeException("Value cannot be empty.");
            }

            string digits = new string(cnpj.Where(char.IsDigit).ToArray()).PadLeft(Constants.CNPJ_LENGTH_WITHOUT_FORMATTING, '0');

            return string.Format(Constants.CNPJ_MASK, digits[0], digits[1], digits[2], digits[3], digits[4], digits[5], digits[6], digits[7], digits[8], digits[9], digits[10], digits[11], digits[12], digits[13]);
        }

        private static char Mod11(string number)
        {
            long returnValue = 0;

            for (int i = number.Length - 1, multiplier = 2; i >= 0; --i, ++multiplier)
            {
                returnValue += int.Parse(number[i].ToString(), CultureInfo.InvariantCulture) * multiplier;

                if (multiplier == 9) multiplier = 1;
            }

            var mod11 = returnValue % 11;
            return mod11 < 2 ? '0' : (11 - mod11).ToString(CultureInfo.InvariantCulture)[0];
        }

        private static bool HasRepeatedDigits(string cnpj)
        {
            string[] invalidNumbers =
            {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };

            return invalidNumbers.Contains(cnpj);
        }
    }
}
