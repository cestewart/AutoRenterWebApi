using System.Collections.Generic;
using Api.Commands.Branding;
using Api.Models;
using Data;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Commands.Branding
{
    [TestFixture]
    public class GetBrandingTests
    {
        private static FakeDbSet<Data.Branding> GetMockedBrandingData()
        {
            var brandings = new List<Data.Branding>
            {
                new Data.Branding
                {
                    BrandingId = 101,
                    Item = "Logo",
                    ContentType = "image/jpeg",
                    Image = new byte[] {1, 2, 3, 4, 5}
                },
                new Data.Branding
                {
                    BrandingId = 102,
                    Item = "LandingPageImage",
                    ContentType = "image/jpeg",
                    Image = new byte[] {1, 2, 3, 4, 5}
                }
            };
            var brandingDbSet = new FakeDbSet<Data.Branding>();
            brandingDbSet.SetData(brandings);
            return brandingDbSet;
        }

        [Test]
        public void should_return_branding_model_from_execute()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Brandings).Returns(GetMockedBrandingData().Object).Verifiable();

            var mockGetBranding = new Mock<GetBranding>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockGetBranding.Object.Execute("Logo");

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Message);
            mockGetBranding.VerifyAll();
        }

        [Test]
        public void should_return_branding_model_with_error_from_execute()
        {
            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Brandings).Returns(GetMockedBrandingData().Object).Verifiable();

            var mockGetBranding = new Mock<GetBranding>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockGetBranding.Object.Execute("Foo");

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);
            Assert.AreEqual("The branding 'Foo' could not be found.", result.Message);
            mockGetBranding.VerifyAll();
        }
    }
}
