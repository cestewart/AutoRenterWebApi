using Api.Converters;
using Api.Models;
using NUnit.Framework;

namespace Api.Tests.Converters
{
    [TestFixture]
    public class UserModelConverterTests
    {
        [Test]
        public void should_return_user_model_from_convert_database_user_model_to_api_user_model()
        {
            var user = new Data.User
            {
                UserId = 101,
                FirstName = "John",
                LastName = "Doe",
                Username = "jdoe",
                Email = "jdoe@gmail.com",
                LdapEnabled = false,
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
        }

        [Test]
        public void should_return_null_from_convert_database_user_model_to_api_user_model()
        {
            var result = UserModelConverter.ConvertDatabaseUserModelToApiUserModel(null);

            Assert.IsNull(result);
        }


        [Test]
        public void should_return_database_user_model_from_convert_api_user_model_to_database_user_model()
        {
            var user = new UserModel
            {
                UserId = 101,
                FirstName = "John",
                LastName = "Doe",
                Username = "jdoe",
                Email = "jdoe@gmail.com",
                LdapEnabled = false,
                UserAdministrator = true,
                BrandingAdministrator = true,
                FleetAdministrator = true
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
        }

        [Test]
        public void should_return_null_from_ConvertApiUserModelToDatabaseUserModel()
        {
            var result = UserModelConverter.ConvertApiUserModelToDatabaseUserModel(null);

            Assert.IsNull(result);
        }

        [Test]
        public void should_return_updated_database_user_model_from_convert_api_user_model_to_database_user_model()
        {
            var user = new UserModel
            {
                UserId = 101,
                FirstName = "John",
                LastName = "Doe",
                Username = "jdoe",
                Email = "jdoe@gmail.com",
                LdapEnabled = false,
                UserAdministrator = true,
                BrandingAdministrator = true,
                FleetAdministrator = true
            };

            var databaseUser = new Data.User
            {
                UserId = 333,
                FirstName = "xxxx",
                LastName = "xxxx",
                Username = "xxxx",
                Email = "xxxx",
                LdapEnabled = true,
                UserAdministrator = false,
                BrandingAdministrator = false,
                FleetAdministrator = false
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
        public void should_return_null_when_updating_database_user_model_from_convert_api_user_model_to_database_user_model()
        {
            var result = UserModelConverter.ConvertApiUserModelToDatabaseUserModel(null, new Data.User());

            Assert.IsNull(result);
        }
    }
}
