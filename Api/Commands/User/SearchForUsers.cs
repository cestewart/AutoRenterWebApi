using System.Collections.Generic;
using System.Linq;
using Api.Converters;
using Api.Models;
using Data;

namespace Api.Commands.User
{
    public class SearchForUsers : ISearchForUsers
    {
        private readonly AutoRenterDatabaseContext _autoRenterDatabaseContext;

        public SearchForUsers(AutoRenterDatabaseContext autoRenterDatabaseContext)
        {
            _autoRenterDatabaseContext = autoRenterDatabaseContext;
        }

        public ResultModel Execute(string searchTerm)
        {
            var users = SearshDatabaseForUsers(searchTerm);

            return new ResultModel
            {
                Data = users.Count > 0 ? users.Select(UserModelConverter.ConvertDatabaseUserModelToApiUserModel).ToList() : null,
                Success = true
            };
        }

        public virtual List<Data.User> SearshDatabaseForUsers(string searchTerm)
        {
            return _autoRenterDatabaseContext.Users.Where(i => i.Username.Contains(searchTerm)
                                                               || i.Email.Contains(searchTerm)
                                                               || i.FirstName.Contains(searchTerm)
                                                               || i.LastName.Contains(searchTerm)
                ).ToList();
        }
    }
}