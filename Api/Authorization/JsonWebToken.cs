namespace Api.Authorization
{
    public class JsonWebToken : IJsonWebToken
    {
        public string CreateToken()
        {
            return "coming soon a jwt token!";
        }

        public bool IsTokenValid(string token)
        {
            return true;
        }
    }
}