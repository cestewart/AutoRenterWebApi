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
    }
}
