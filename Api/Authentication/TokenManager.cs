using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using Data;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Api.Authentication
{
    public class TokenManager : ITokenManager
    {
        private const string Secret = "KFeAyTWgrqA7J4rB5dKvR8FmthcNvu2H9GQuZXQG7wWg6qVbazXZ6kzEt5HVDYwp";

        public InMemorySymmetricSecurityKey SecurityKey => new InMemorySymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));

        public SigningCredentials SigningCredentials = new SigningCredentials(new InMemorySymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret)), SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);

        public string AppliesToAddress => "http://my.website.com";

        public string TokenIssuerName => "AutoRenter";

        public DateTime CurrentDateTime => DateTime.Now;

        public virtual string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var plainToken = tokenHandler.CreateToken(GetSecurityTokenDescriptor(user));
            return tokenHandler.WriteToken(plainToken);
        }

        public virtual SecurityTokenDescriptor GetSecurityTokenDescriptor(User user)
        {
            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                AppliesToAddress = AppliesToAddress,
                TokenIssuerName = TokenIssuerName,
                Subject = GetClaims(user),
                SigningCredentials = SigningCredentials,
                Lifetime = new Lifetime(CurrentDateTime, CurrentDateTime.AddMinutes(2))
            };
            return securityTokenDescriptor;
        }

        public virtual ClaimsIdentity GetClaims(User user)
        {
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim("Username", user.Username),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("BrandingAdministrator", user.BrandingAdministrator.ToString()),
                new Claim("FleetAdministrator", user.FleetAdministrator.ToString()),
                new Claim("UserAdministrator", user.UserAdministrator.ToString())
            }, "Custom");
            return claimsIdentity;
        }

        public virtual bool IsTokenValid(string token)
        {
            try
            {
                var tokenValidationParameters = new TokenValidationParameters()
                {
                    ValidAudiences = new[] { AppliesToAddress },
                    ValidIssuers = new[] { TokenIssuerName },
                    IssuerSigningKey = SecurityKey
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken validatedToken;
                tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}