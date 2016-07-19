using System.Linq;
using Api.Converters;
using Api.Models;
using Data;

namespace Api.Commands.User
{
    public class GetUser : IGetUser
    {
        private readonly AutoRenterDatabaseContext _autoRenterDatabaseContext;

        public GetUser(AutoRenterDatabaseContext autoRenterDatabaseContext)
        {
            _autoRenterDatabaseContext = autoRenterDatabaseContext;
        }

        public ResultModel Execute(int userId)
        {
            var user = _autoRenterDatabaseContext.Users.FirstOrDefault(i => i.UserId == userId);

            return new ResultModel
            {
                Data = UserModelConverter.ConvertDatabaseUserModelToApiUserModel(user),
                Success = user != null,
                Message = user == null ? "The user could not be found." : null
            };
        }
    }
}