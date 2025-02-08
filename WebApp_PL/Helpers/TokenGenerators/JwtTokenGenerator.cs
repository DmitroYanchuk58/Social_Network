using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApp_PL.Helpers.TokenGenerators;

namespace WebApp_PL.Helpers
{
    public class JwtTokenGenerator : ITokenGenerator
    {
        public string GenerateToken(string username)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Dmitro_Yanchuk_Secure_Long_Secret_Key_123!"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static SymmetricSecurityKey GetKey(WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;
            var secretKey = configuration["JwtSettings:SecretKey"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            return key;
        }

    }
}
