using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Text;
using System.Security.Claims;
using PoolGame.Models;
using Microsoft.Extensions.Configuration;
using PoolGame.Services.Helpers.Interfaces;

namespace PoolGameAPI.Controllers
{
    public sealed class TokenProvider : ITokenProvider
    {
        public readonly IConfiguration _configuration;
        public TokenProvider(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string CreateToken(User user)
        {

            string secretKey = _configuration["Jwt:Secret"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDesctiptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),




                ]),
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = credentials,
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]

            };

            var handler = new JsonWebTokenHandler();

            string token = handler.CreateToken(tokenDesctiptor);
            return token;
        }

        public string getUsernameFromToken(string token)
        {
            token = token.Substring("Bearer ".Length);
            var handler = new JsonWebTokenHandler();
            var jwtToken = handler.ReadJsonWebToken(token);

            var usernameClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "sub");

            return usernameClaim.Value;
        }
        public int getUserIdFromToken(string token)
        {
            token = token.Substring("Bearer ".Length);
            var handler = new JsonWebTokenHandler();
            var jwtToken = handler.ReadJsonWebToken(token);

            var idClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);

            if (idClaim != null && int.TryParse(idClaim.Value, out int userId))
            {
                return userId;
            }
            throw new Exception("There is no userId in the claim");
        }

    }
}
