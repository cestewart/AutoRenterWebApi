using System.Linq;
using Api.Converters;
using Api.Models;
using Data;

namespace Api.Commands.User
{
    public class SaveUser : ISaveUser
    {
        private readonly AutoRenterDatabaseContext _autoRenterDatabaseContext;

        public SaveUser(AutoRenterDatabaseContext autoRenterDatabaseContext)
        {
            _autoRenterDatabaseContext = autoRenterDatabaseContext;
        }

        public virtual ResultModel Execute(UserModel userModel)
        {
            return userModel.UserId == 0 ? CreateUser(userModel) : UpdateUser(userModel);
        }

        public virtual ResultModel CreateUser(UserModel userModel)
        {
            var user = UserModelConverter.ConvertApiUserModelToDatabaseUserModel(userModel);
            _autoRenterDatabaseContext.Users.Add(user);
            _autoRenterDatabaseContext.SaveChanges();
            return new ResultModel
            {
                Data = UserModelConverter.ConvertDatabaseUserModelToApiUserModel(user),
                Success = true
            };
        }

        public virtual ResultModel UpdateUser(UserModel userModel)
        {
            var user = _autoRenterDatabaseContext.Users.FirstOrDefault(i => i.UserId == userModel.UserId);
            UserModelConverter.ConvertApiUserModelToDatabaseUserModel(userModel, user);
            _autoRenterDatabaseContext.SaveChanges();
            return new ResultModel
            {
                Data = userModel,
                Success = true
            };
        }
    }
}