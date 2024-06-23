namespace StringSums.Tests
{
    public class StringSumTests
    {
        private readonly IStringSum stringSum = new StringSum();

        [Fact]
        public void Sum_EmptyStrings_ReturnsZero()
        {
            Assert.Equal("0", stringSum.Sum("", ""));
        }

        [Fact]
        public void Sum_ValidNumbers_ReturnsSum()
        {
            Assert.Equal("3", stringSum.Sum("1", "2"));
            Assert.Equal("0", stringSum.Sum("0", "0"));
        }

        [Fact]
        public void Sum_InvalidNumbers_ReturnsZero()
        {
            Assert.Equal("0", stringSum.Sum("a", "b"));
            Assert.Equal("1", stringSum.Sum("1", "b"));
        }
    }
}
