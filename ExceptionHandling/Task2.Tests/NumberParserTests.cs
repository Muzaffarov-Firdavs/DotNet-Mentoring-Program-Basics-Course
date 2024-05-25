using System;
using NUnit.Framework;

namespace Task2.Tests
{
    [TestFixture]
    public class NumberParserTests
    {
        private readonly INumberParser _parser = new NumberParser();

        [TestCase("0", ExpectedResult = 0)]
        [TestCase("+0", ExpectedResult = 0)]
        [TestCase("-0", ExpectedResult = 0)]
        [TestCase("+13230", ExpectedResult = 13230)]
        [TestCase("163042", ExpectedResult = 163042)]
        [TestCase("-10", ExpectedResult = -10)]
        [TestCase("007", ExpectedResult = 7)]
        [TestCase("+007", ExpectedResult = 7)]
        [TestCase("-007", ExpectedResult = -7)]
        [TestCase("-2147483648", ExpectedResult = int.MinValue)]
        [TestCase("2147483647", ExpectedResult = int.MaxValue)]
        [TestCase("-12034", ExpectedResult = -12034)]
        [TestCase("-12034    ", ExpectedResult = -12034)]
        public int Parse_ValidNumberString_ReturnsInt32Value(string stringValue)
        {
            return _parser.Parse(stringValue);
        }

        [Test]
        public void Parse_Null_ThrowArgumentNullException()
        {
            string stringValue = null;

            Assert.That(() => _parser.Parse(stringValue), Throws.ArgumentNullException);
        }

        [TestCase("")]
        [TestCase("  ")]
        [TestCase("1,390,146")]
        [TestCase("$190,235,421,127")]
        [TestCase("0xFA1B")]
        [TestCase("0xFA1B")]
        [TestCase("16e07")]
        [TestCase("134985.0")]
        [TestCase("-+12034")]
        [TestCase("+-12034")]
        [TestCase("0-12034")]
        public void Parse_InvalidNumberFormat_ThrowFormatException(string stringValue)
        {
            Assert.That(() => _parser.Parse(stringValue), Throws.InstanceOf<FormatException>());
        }

        [TestCase("2147483648")]
        [TestCase("-2147483649")]
        [TestCase("9999999999999999")]
        [TestCase("-9999999999999999")]
        public void Parse_NumberOutOfInt32Range_ThrowFormatException(string stringValue)
        {
            Assert.That(() => _parser.Parse(stringValue), Throws.InstanceOf<OverflowException>());
        }
    }
}