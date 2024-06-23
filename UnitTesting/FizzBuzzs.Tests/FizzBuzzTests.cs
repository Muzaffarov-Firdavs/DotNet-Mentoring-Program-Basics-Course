namespace FizzBuzzs.Tests
{
    public class FizzBuzzTests
    {
        private readonly IFizzBuzz fizzBuzz = new FizzBuzz();

        [Fact]
        public void ReturnsNumberForNonMultiples()
        {
            Assert.Equal("1", fizzBuzz.GetFizzBuzz(1));
            Assert.Equal("2", fizzBuzz.GetFizzBuzz(2));
        }

        [Fact]
        public void ReturnsFizzForMultiplesOfThree()
        {
            Assert.Equal("Fizz", fizzBuzz.GetFizzBuzz(3));
            Assert.Equal("Fizz", fizzBuzz.GetFizzBuzz(6));
        }

        [Fact]
        public void ReturnsBuzzForMultiplesOfFive()
        {
            Assert.Equal("Buzz", fizzBuzz.GetFizzBuzz(5));
            Assert.Equal("Buzz", fizzBuzz.GetFizzBuzz(10));
        }

        [Fact]
        public void ReturnsFizzBuzzForMultiplesOfThreeAndFive()
        {
            Assert.Equal("FizzBuzz", fizzBuzz.GetFizzBuzz(15));
            Assert.Equal("FizzBuzz", fizzBuzz.GetFizzBuzz(30));
        }

        [Fact]
        public void ReturnsCorrectRange()
        {
            var expected = new[] { "1", "2", "Fizz", "4", "Buzz", "Fizz", "7", "8", "Fizz", "Buzz", "11", "Fizz", "13", "14", "FizzBuzz" };
            Assert.Equal(expected, fizzBuzz.GetFizzBuzzRange(1, 15).ToArray());
        }
    }
}
