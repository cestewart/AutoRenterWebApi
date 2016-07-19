using Api.Models;

namespace Api.Commands.User
{
    public interface IDeleteUser
    {
        ResultModel Execute(int userId);
    }
}
