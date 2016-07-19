namespace Api.Authorization
{
    public interface IJsonWebToken
    {
        string CreateToken();

        bool IsTokenValid(string token);
    }
}
