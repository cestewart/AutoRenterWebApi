using Api.Models;

namespace Api.Commands.User
{
    public interface ISearchForUsers
    {
        ResultModel Execute(string searchTerm);
    }
}
