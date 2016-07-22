using Api.Converters;
using NUnit.Framework;

namespace Api.Tests.Converters
{
    [TestFixture]
    public class DataTypeConverterTests
    {
        [Test]
        public void should_return_int_from_ToInt()
        {
            Assert.AreEqual(5, DataTypeConverter.ToInt("5"));
            Assert.AreEqual(2631232, DataTypeConverter.ToInt("2631232"));
        }

        [Test]
        public void should_return_negative_one_from_ToInt()
        {
            Assert.AreEqual(-1, DataTypeConverter.ToInt(""));
            Assert.AreEqual(-1, DataTypeConverter.ToInt("foo"));
        }

        [Test]
        public void should_return_true_from_ToBool()
        {
            Assert.IsTrue(DataTypeConverter.ToBool("true"));
            Assert.IsTrue(DataTypeConverter.ToBool("True"));
            Assert.IsTrue(DataTypeConverter.ToBool("TRUE"));
        }

        [Test]
        public void should_return_false_from_ToBool()
        {
            Assert.IsFalse(DataTypeConverter.ToBool("false"));
            Assert.IsFalse(DataTypeConverter.ToBool("False"));
            Assert.IsFalse(DataTypeConverter.ToBool("FALSE"));
            Assert.IsFalse(DataTypeConverter.ToBool("Foo"));
        }
    }
}
