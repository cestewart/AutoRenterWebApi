using Api.Models;

namespace Api.Converters
{
    public static class UserModelConverter
    {
        public static UserModel ConvertDatabaseUserModelToApiUserModel(Data.User user)
        {
            if (user == null) return null;

            return new UserModel
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.Username,
                UserAdministrator = user.UserAdministrator,
                FleetAdministrator = user.FleetAdministrator,
                BrandingAdministrator = user.BrandingAdministrator
            };
        }

        public static Data.User ConvertApiUserModelToDatabaseUserModel(UserModel user)
        {
            return user == null ? null : ConvertApiUserModelToDatabaseUserModel(user, new Data.User());
        }

        public static Data.User ConvertApiUserModelToDatabaseUserModel(UserModel user, Data.User databaseUser)
        {
            if (user == null) return null;

            databaseUser.UserId = user.UserId;
            databaseUser.FirstName = user.FirstName;
            databaseUser.LastName = user.LastName;
            databaseUser.Email = user.Email;
            databaseUser.Md5HashOfPassword = user.Password;
            databaseUser.Username = user.Username;
            databaseUser.UserAdministrator = user.UserAdministrator;
            databaseUser.FleetAdministrator = user.FleetAdministrator;
            databaseUser.BrandingAdministrator = user.BrandingAdministrator;

            return databaseUser;
        }
    }
}