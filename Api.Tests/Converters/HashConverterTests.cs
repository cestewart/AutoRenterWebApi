using Api.Converters;
using NUnit.Framework;

namespace Api.Tests.Converters
{
    [TestFixture]
    public class HashConverterTests
    {
        [Test]
        public void should_return_hashed_string_from_ToHash()
        {
            Assert.AreEqual("C3FCD3D76192E4007DFB496CCA67E13B", HashConverter.ToHash("abcdefghijklmnopqrstuvwxyz"));
        }

        [Test]
        public void should_return_null_from_ToHash()
        {
            Assert.IsNull(HashConverter.ToHash(string.Empty));
        }
    }
}
