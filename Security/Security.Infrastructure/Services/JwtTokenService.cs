using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Security.Core.Configs;
using Security.Core.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Security.Infrastructure.Services
{
    public class JwtTokenService(IOptions<JwtConfig> options) : IJwtTokenService
    {
        private readonly JwtConfig _jwtConfig = options.Value;

        public string GenerateJwtToken(string name, string email, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var securityToken = new SecurityTokenDescriptor
            {
                Audience = _jwtConfig.Audience,
                Issuer = _jwtConfig.Issuer,
                Expires = DateTime.Now.AddMinutes(_jwtConfig.DurationInMinutes),
                SigningCredentials = credentials,
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role)
                ])
            };

            var tokenhandler = new JwtSecurityTokenHandler();

            var token = tokenhandler.CreateToken(securityToken);

            return tokenhandler.WriteToken(token);
        }
    }
}
