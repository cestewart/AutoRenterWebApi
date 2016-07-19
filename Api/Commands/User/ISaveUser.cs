using Api.Models;

namespace Api.Commands.User
{
    public interface ISaveUser
    {
        ResultModel Execute(UserModel userModel);
    }
}
