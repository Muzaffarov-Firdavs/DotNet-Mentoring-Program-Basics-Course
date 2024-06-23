namespace FizzBuzzs
{
    public class FizzBuzz : IFizzBuzz
    {
        public string GetFizzBuzz(int number)
        {
            if (number % 15 == 0) return "FizzBuzz";
            if (number % 3 == 0) return "Fizz";
            if (number % 5 == 0) return "Buzz";
            return number.ToString();
        }

        public IEnumerable<string> GetFizzBuzzRange(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                yield return GetFizzBuzz(i);
            }
        }
    }
}
