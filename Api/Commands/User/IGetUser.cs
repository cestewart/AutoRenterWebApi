using Api.Models;

namespace Api.Commands.User
{
    public interface IGetUser
    {
        ResultModel Execute(int userId);
    }
}
