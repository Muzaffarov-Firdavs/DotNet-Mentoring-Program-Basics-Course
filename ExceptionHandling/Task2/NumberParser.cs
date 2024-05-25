using System;

namespace Task2
{
    public class NumberParser : INumberParser
    {
        public int Parse(string stringValue)
        {
            if (stringValue is null)
                throw new ArgumentNullException("(stringValue) cannot be null.");

            if (stringValue.Trim() == string.Empty)
                throw new FormatException("(stringValue) cannot be empty.");

            int result = 0;
            int sign = 1;
            int startIndex = 0;

            stringValue = stringValue.Trim();

            if (stringValue[0] == '-' || stringValue[0] == '+')
            {
                sign = (stringValue[0] == '-') ? -1 : 1;
                startIndex = 1;
            }

            for (int i = startIndex; i < stringValue.Length; i++)
            {
                if (stringValue[i] < '0' || stringValue[i] > '9')
                {
                    throw new FormatException($"Invalid character in (stringValue).");
                }

                int digit = stringValue[i] - '0';

                if (sign == 1 && (result > (int.MaxValue - digit) / 10))
                {
                    throw new OverflowException("(stringValue) is too large for an Int32.");
                }
                else if (sign == -1 && (result * sign < (int.MinValue + digit) / 10))
                {
                    throw new OverflowException("(stringValue) is too minus large for an Int32.");
                }

                result = result * 10 + digit;
            }

            return result * sign;
        }
    }
}