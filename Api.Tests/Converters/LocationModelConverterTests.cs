using System.Collections.Generic;
using Api.Converters;
using Api.Models;
using Data;
using NUnit.Framework;

namespace Api.Tests.Converters
{
    [TestFixture]
    public class LocationModelConverterTests
    {
        [Test]
        public void should_return_location_model_from_convert_database_location_model_to_api_location_model()
        {
            var location = new Location
            {
                LocationId = 101,
                Name = "Location One",
                City = "Indy",
                SiteId = "LOI",
                State = new State {Name = "Indiana", Abbreviation = "IN"},
                Vehicles = new List<Vehicle> {new Vehicle {VehicleId = 201, Make = "Toyota"}, new Vehicle {VehicleId = 202, Make = "Ford"}}
            };

            var result = LocationModelConverter.ConvertDatabaseLocationModelToApiLocationModel(location);

            Assert.AreEqual(location.LocationId, result.LocationId);
            Assert.AreEqual(location.Name, result.Name);
            Assert.AreEqual(location.City, result.City);
            Assert.AreEqual(location.SiteId, result.SiteId);
            Assert.AreEqual(location.State.Name, result.State);
            Assert.AreEqual(location.State.Abbreviation, result.StateAbbreviation);
            Assert.AreEqual(2, result.VehicleCount);
        }

        [Test]
        public void should_return_LocationModel_from_ConvertDatabaseLocationModelToApiLocationModel_without_vehicle_count()
        {
            var location = new Location
            {
                LocationId = 101,
                Name = "Location One",
                City = "Indy",
                SiteId = "LOI",
                State = new State { Name = "Indiana", Abbreviation = "IN" }
            };

            var result = LocationModelConverter.ConvertDatabaseLocationModelToApiLocationModel(location);

            Assert.AreEqual(location.LocationId, result.LocationId);
            Assert.AreEqual(location.Name, result.Name);
            Assert.AreEqual(location.City, result.City);
            Assert.AreEqual(location.SiteId, result.SiteId);
            Assert.AreEqual(location.State.Name, result.State);
            Assert.AreEqual(location.State.Abbreviation, result.StateAbbreviation);
            Assert.AreEqual(0, result.VehicleCount);
        }

        [Test]
        public void should_return_LocationModel_from_ConvertDatabaseLocationModelToApiLocationModel_without_state()
        {
            var location = new Location
            {
                LocationId = 101,
                Name = "Location One",
                City = "Indy",
                SiteId = "LOI"
            };

            var result = LocationModelConverter.ConvertDatabaseLocationModelToApiLocationModel(location);

            Assert.AreEqual(location.LocationId, result.LocationId);
            Assert.AreEqual(location.Name, result.Name);
            Assert.AreEqual(location.City, result.City);
            Assert.AreEqual(location.SiteId, result.SiteId);
            Assert.AreEqual("Unknown", result.State);
            Assert.AreEqual("NA", result.StateAbbreviation);
            Assert.AreEqual(0, result.VehicleCount);
        }

        [Test]
        public void should_return_database_location_model_from_ConvertApiLocationModelToDatabaseLocationModel()
        {
            var locationModel = new LocationModel
            {
                Name = "Location One",
                City = "Indy",
                SiteId = "LOI",
                StateId = 201
            };

            var result = LocationModelConverter.ConvertApiLocationModelToDatabaseLocationModel(locationModel);

            Assert.AreEqual(locationModel.Name, result.Name);
            Assert.AreEqual(locationModel.City, result.City);
            Assert.AreEqual(locationModel.SiteId, result.SiteId);
            Assert.AreEqual(locationModel.StateId, result.StateId);
        }

        [Test]
        public void should_return_updated_database_location_model_from_ConvertApiLocationModelToDatabaseLocationModel()
        {
            var locationModel = new LocationModel
            {
                Name = "Location One",
                City = "Indy",
                SiteId = "LOI",
                StateId = 201
            };

            var location = new Data.Location
            {
                Name = "xxxx",
                City = "xxxx",
                SiteId = "zzz",
                StateId = 444
            };

            var result = LocationModelConverter.ConvertApiLocationModelToDatabaseLocationModel(locationModel, location);

            Assert.AreEqual(locationModel.Name, result.Name);
            Assert.AreEqual(locationModel.City, result.City);
            Assert.AreEqual(locationModel.SiteId, result.SiteId);
            Assert.AreEqual(locationModel.StateId, result.StateId);
        }
    }
}
