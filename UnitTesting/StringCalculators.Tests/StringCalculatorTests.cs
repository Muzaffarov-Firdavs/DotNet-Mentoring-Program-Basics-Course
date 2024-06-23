namespace StringCalculators.Tests
{
    public class StringCalculatorTests
    {
        private readonly IStringCalculator stringCalculator = new StringCalculator();

        [Fact]
        public void Add_EmptyString_ReturnsZero()
        {
            Assert.Equal(0, stringCalculator.Add(""));
        }

        [Fact]
        public void Add_SingleNumber_ReturnsNumber()
        {
            Assert.Equal(1, stringCalculator.Add("1"));
        }

        [Fact]
        public void Add_TwoNumbers_ReturnsSum()
        {
            Assert.Equal(3, stringCalculator.Add("1,2"));
        }

        [Fact]
        public void Add_MultipleNumbers_ReturnsSum()
        {
            Assert.Equal(6, stringCalculator.Add("1,2,3"));
        }

        [Fact]
        public void Add_NewLineDelimiter_ReturnsSum()
        {
            Assert.Equal(6, stringCalculator.Add("1\n2,3"));
        }

        [Fact]
        public void Add_CustomDelimiter_ReturnsSum()
        {
            Assert.Equal(3, stringCalculator.Add("//;\n1;2"));
        }

        [Fact]
        public void Add_Negatives_ThrowsException()
        {
            var ex = Assert.Throws<ArgumentException>(() => stringCalculator.Add("1,-2,3"));
            Assert.Contains("Negatives not allowed: -2", ex.Message);
        }
    }
}
