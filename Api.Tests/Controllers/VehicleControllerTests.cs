using System.Collections.Specialized;
using Api.Commands.Vehicle;
using Api.Controllers;
using Moq;
using NUnit.Framework;

namespace Api.Tests.Controllers
{
    [TestFixture]
    public class VehicleControllerTests
    {
        private Mock<IErrorHandler> _stubErrorHandler;
        private Mock<IGetVehicle> _stubGetVehicle;
        private Mock<ISaveVehicle> _stubSaveVehicle;
        private Mock<IDeleteVehicle> _stubDeleteVehicle;

        [SetUp]
        public void SetUp()
        {
            _stubErrorHandler = new Mock<IErrorHandler>();
            _stubGetVehicle = new Mock<IGetVehicle>();
            _stubSaveVehicle = new Mock<ISaveVehicle>();
            _stubDeleteVehicle = new Mock<IDeleteVehicle>();
        }

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

            var mockVehicleController = new Mock<VehicleController>(_stubErrorHandler.Object, _stubGetVehicle.Object, _stubSaveVehicle.Object, _stubDeleteVehicle.Object) { CallBase = true };

            var result = mockVehicleController.Object.GetVehicleModelData(form);

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
