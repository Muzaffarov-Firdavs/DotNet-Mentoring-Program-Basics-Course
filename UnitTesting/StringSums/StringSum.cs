namespace StringSums
{
    public class StringSum : IStringSum
    {
        public string Sum(string num1, string num2)
        {
            if (!int.TryParse(num1, out int n1)) n1 = 0;
            if (!int.TryParse(num2, out int n2)) n2 = 0;

            return (n1 + n2).ToString();
        }
    }
}
