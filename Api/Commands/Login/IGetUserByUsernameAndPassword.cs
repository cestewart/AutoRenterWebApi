using Api.Models;

namespace Api.Commands.Login
{
    public interface IGetUserByUsernameAndPassword
    {
        ResultModel Execute(LoginModel loginModel);
    }
}
