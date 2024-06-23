namespace StringCalculators
{
    public class StringCalculator : IStringCalculator
    {
        public int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers))
                return 0;

            var delimiters = new[] { ',', '\n' };
            if (numbers.StartsWith("//"))
            {
                var parts = numbers.Split('\n', 2);
                delimiters = new[] { parts[0][2] };
                numbers = parts[1];
            }

            var nums = numbers.Split(delimiters, StringSplitOptions.None).Select(int.Parse);
            var negatives = nums.Where(n => n < 0).ToArray();

            if (negatives.Any())
                throw new ArgumentException($"Negatives not allowed: {string.Join(", ", negatives)}");

            return nums.Sum();
        }
    }
}
