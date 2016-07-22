using Data;

namespace Api.Authentication
{
    public interface ITokenManager
    {
        string CreateToken(User user);

        bool IsTokenValid(string token);
    }
}
