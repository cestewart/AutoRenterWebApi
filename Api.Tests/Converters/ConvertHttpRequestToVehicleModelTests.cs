using System.Collections.Specialized;
using Api.Converters;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Converters
{
    [TestFixture]
    public class ConvertHttpRequestToVehicleModelTests
    {
        [Test]
        public void should_return_VehicleModel_from_GetVehicleModelData()
        {
            var form = new NameValueCollection
            {
                {"VehicleId", "1001"},
                {"Vin", "V1234XXXXXX"},
                {"Model", "Civic"},
                {"Year", "2016"},
                {"Miles", "23214"},
                {"Color", "Blue"},
                {"LocationId", "10"},
                {"RentToOwn", "true"},
                {"Make", "Honda"}
            };

            var mockConvertHttpRequestToVehicleModel = new Mock<ConvertHttpRequestToVehicleModel> { CallBase = true };

            var result = mockConvertHttpRequestToVehicleModel.Object.GetVehicleModelData(form);

            Assert.AreEqual(1001, result.VehicleId);
            Assert.AreEqual("V1234XXXXXX", result.Vin);
            Assert.AreEqual("Civic", result.Model);
            Assert.AreEqual(2016, result.Year);
            Assert.AreEqual(23214, result.Miles);
            Assert.AreEqual("Blue", result.Color);
            Assert.AreEqual(10, result.LocationId);
            Assert.IsTrue(result.RentToOwn);
            Assert.AreEqual("Honda", result.Make);
        }
    }
}
