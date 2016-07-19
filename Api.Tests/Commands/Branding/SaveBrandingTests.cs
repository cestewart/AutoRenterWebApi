using System.Collections.Generic;
using System.Configuration.Internal;
using System.IO;
using Api.Commands.Branding;
using Api.Converters;
using Api.Models;
using Data;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Commands.Branding
{
    [TestFixture]
    public class SaveBrandingTests
    {
        private Mock<AutoRenterDatabaseContext> _stubAutoRenterDatabaseContext;

        [SetUp]
        public void SetUp()
        {
            _stubAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext>();
        }

        private static FakeDbSet<Data.Branding> GetMockedBrandingData()
        {
            var brandings = new List<Data.Branding>
            {
                new Data.Branding
                {
                    BrandingId = 101,
                    Item = "logo"
                },
                new Data.Branding
                {
                    BrandingId = 102,
                    Item = "landingpageimage"
                }
            };
            var brandingDbSet = new FakeDbSet<Data.Branding>();
            brandingDbSet.SetData(brandings);
            return brandingDbSet;
        }

        [Test]
        public void should_save_branding_record_to_database()
        {
            var brandingModel = new BrandingModel
            {
                Item = "Logo",
                Image = StreamConverter.ConvertByteArrayToStream(new byte[] { 0, 0, 1, 2, 3 })
            };

            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Brandings).Returns(GetMockedBrandingData().Object).Verifiable();
            mockAutoRenterDatabaseContext.Setup(i => i.SaveChanges()).Verifiable();

            var mockSaveBranding = new Mock<SaveBranding>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockSaveBranding.Object.SaveBrandingRecord(brandingModel);

            Assert.IsTrue(result);
            mockAutoRenterDatabaseContext.VerifyAll();
        }

        [Test]
        public void should_fail_to_save_branding_record_to_database()
        {
            var brandingModel = new BrandingModel
            {
                Item = "LogoFoo",
                Image = StreamConverter.ConvertByteArrayToStream(new byte[] { 0, 0, 1, 2, 3 })
            };

            var mockAutoRenterDatabaseContext = new Mock<AutoRenterDatabaseContext> { CallBase = true };
            mockAutoRenterDatabaseContext.Setup(i => i.Brandings).Returns(GetMockedBrandingData().Object).Verifiable();

            var mockSaveBranding = new Mock<SaveBranding>(mockAutoRenterDatabaseContext.Object) { CallBase = true };

            var result = mockSaveBranding.Object.SaveBrandingRecord(brandingModel);

            Assert.IsFalse(result);
            mockAutoRenterDatabaseContext.VerifyAll();
        }

        [Test]
        public void should_return_successful_result_model()
        {
            var brandingModel = new BrandingModel
            {
                Item = "Logo",
                Image = StreamConverter.ConvertByteArrayToStream(new byte[] { 0, 0, 1, 2, 3 })
            };

            var mockSaveBranding = new Mock<SaveBranding>(_stubAutoRenterDatabaseContext.Object) { CallBase = true };
            mockSaveBranding.Setup(i => i.SaveBrandingRecord(It.IsAny<BrandingModel>())).Returns(true).Verifiable();

            var result = mockSaveBranding.Object.Execute(brandingModel);

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Message);
            mockSaveBranding.VerifyAll();
        }

        [Test]
        public void should_return_failed_result_model()
        {
            var brandingModel = new BrandingModel
            {
                Item = "Logo",
                Image = StreamConverter.ConvertByteArrayToStream(new byte[] { 0, 0, 1, 2, 3 })
            };

            var mockSaveBranding = new Mock<SaveBranding>(_stubAutoRenterDatabaseContext.Object) { CallBase = true };
            mockSaveBranding.Setup(i => i.SaveBrandingRecord(It.IsAny<BrandingModel>())).Returns(false).Verifiable();

            var result = mockSaveBranding.Object.Execute(brandingModel);

            Assert.IsInstanceOf<ResultModel>(result);
            Assert.IsFalse(result.Success);
            Assert.AreEqual("The branding item 'Logo' could not be found.", result.Message);
            mockSaveBranding.VerifyAll();
        }
    }
}
