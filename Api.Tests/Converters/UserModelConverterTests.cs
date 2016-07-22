using Api.Converters;
using Api.Models;
using NUnit.Framework;

namespace Api.Tests.Converters
{
    [TestFixture]
    public class UserModelConverterTests
    {
        [Test]
        public void should_return_UserModel_from_ConvertDatabaseUserModelToApiUserModel()
        {
            var user = new Data.User
            {
                UserId = 101,
                FirstName = "John",
                LastName = "Doe",
                Username = "jdoe",
                Email = "jdoe@gmail.com",
                UserAdministrator = true,
                BrandingAdministrator = true,
                FleetAdministrator = true
            };

            var result = UserModelConverter.ConvertDatabaseUserModelToApiUserModel(user);

            Assert.AreEqual(user.UserId, result.UserId);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
            Assert.AreEqual(user.Email, result.Email);
            Assert.AreEqual(user.Username, result.Username);
            Assert.AreEqual(user.UserAdministrator, result.UserAdministrator);
            Assert.AreEqual(user.FleetAdministrator, result.FleetAdministrator);
            Assert.AreEqual(user.BrandingAdministrator, result.BrandingAdministrator);
            Assert.IsNull(result.Password);
        }

        [Test]
        public void should_return_null_from_ConvertDatabaseUserModelToApiUserModel()
        {
            Assert.IsNull(UserModelConverter.ConvertDatabaseUserModelToApiUserModel(null));
        }

        [Test]
        public void should_return_database_user_from_ConvertApiUserModelToDatabaseUserModel()
        {
            var user = new UserModel
            {
                UserId = 101,
                FirstName = "John",
                LastName = "Doe",
                Username = "jdoe",
                Email = "jdoe@gmail.com",
                UserAdministrator = true,
                BrandingAdministrator = true,
                FleetAdministrator = true,
                Password = "secret"
            };

            var result = UserModelConverter.ConvertApiUserModelToDatabaseUserModel(user);

            Assert.AreEqual(user.UserId, result.UserId);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
            Assert.AreEqual(user.Email, result.Email);
            Assert.AreEqual(user.Username, result.Username);
            Assert.AreEqual(user.UserAdministrator, result.UserAdministrator);
            Assert.AreEqual(user.FleetAdministrator, result.FleetAdministrator);
            Assert.AreEqual(user.BrandingAdministrator, result.BrandingAdministrator);
            Assert.AreEqual("5EBE2294ECD0E0F08EAB7690D2A6EE69", result.HashOfPassword);
        }

        [Test]
        public void should_return_null_from_ConvertApiUserModelToDatabaseUserModel()
        {
            Assert.IsNull(UserModelConverter.ConvertApiUserModelToDatabaseUserModel(null));
        }

        [Test]
        public void should_return_updated_database_user_from_ConvertApiUserModelToDatabaseUserModel()
        {
            var user = new UserModel
            {
                UserId = 101,
                FirstName = "John",
                LastName = "Doe",
                Username = "jdoe",
                Email = "jdoe@gmail.com",
                UserAdministrator = true,
                BrandingAdministrator = true,
                FleetAdministrator = true,
                Password = "secret"
            };

            var databaseUser = new Data.User
            {
                UserId = 333,
                FirstName = "xxxx",
                LastName = "xxxx",
                Username = "xxxx",
                Email = "xxxx",
                UserAdministrator = false,
                BrandingAdministrator = false,
                FleetAdministrator = false,
            };

            var result = UserModelConverter.ConvertApiUserModelToDatabaseUserModel(user, databaseUser);

            Assert.AreEqual(user.UserId, result.UserId);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
            Assert.AreEqual(user.Email, result.Email);
            Assert.AreEqual(user.Username, result.Username);
            Assert.AreEqual(user.UserAdministrator, result.UserAdministrator);
            Assert.AreEqual(user.FleetAdministrator, result.FleetAdministrator);
            Assert.AreEqual(user.BrandingAdministrator, result.BrandingAdministrator);
        }

        [Test]
        public void should_return_null_when_updating_database_user_model_from_ConvertApiUserModelToDatabaseUserModel()
        {
            Assert.IsNull(UserModelConverter.ConvertApiUserModelToDatabaseUserModel(null, new Data.User()));
        }
    }
}
