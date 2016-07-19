using System.Linq;
using Api.Models;
using Data;

namespace Api.Commands.User
{
    public class DeleteUser : IDeleteUser
    {
        private readonly AutoRenterDatabaseContext _autoRenterDatabaseContext;

        public DeleteUser(AutoRenterDatabaseContext autoRenterDatabaseContext)
        {
            _autoRenterDatabaseContext = autoRenterDatabaseContext;
        }

        public ResultModel Execute(int userId)
        {
            var user = _autoRenterDatabaseContext.Users.FirstOrDefault(i => i.UserId == userId);

            if (user != null)
            {
                _autoRenterDatabaseContext.Users.Remove(user);
                _autoRenterDatabaseContext.SaveChanges();
            }

            return new ResultModel
            {
                Success = user != null,
                Message = user == null ? "The user could not be found." : null
            };
        }
    }
}