using System.Linq;
using Api.Authentication;
using Api.Converters;
using Api.Models;
using Data;

namespace Api.Commands.Login
{
    public class GetUserByUsernameAndPassword : IGetUserByUsernameAndPassword
    {
        private readonly AutoRenterDatabaseContext _autoRenterDatabaseContext;
        private readonly ITokenManager _tokenManager;

        public GetUserByUsernameAndPassword(AutoRenterDatabaseContext autoRenterDatabaseContext, ITokenManager tokenManager)
        {
            _autoRenterDatabaseContext = autoRenterDatabaseContext;
            _tokenManager = tokenManager;
        }

        public ResultModel Execute(LoginModel loginModel)
        {
            var hashOfPassword = HashConverter.ToHash(loginModel.Password);
            var user = _autoRenterDatabaseContext.Users.FirstOrDefault(i => i.Username == loginModel.Username && i.HashOfPassword == hashOfPassword);

            return new ResultModel
            {
                Data = GetUserModelWithToken(user),
                Success = user != null,
                Message = user == null ? "Login failed.  Please try again." : null
            };
        }

        public virtual UserModel GetUserModelWithToken(Data.User user)
        {
            if (user == null) return null;
            var userModel = UserModelConverter.ConvertDatabaseUserModelToApiUserModel(user);
            userModel.BearerToken = _tokenManager.CreateToken(user);
            return userModel;
        }
    }
}