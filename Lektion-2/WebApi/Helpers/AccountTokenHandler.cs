using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Helpers
{
    public class AccountTokenHandler
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;


        public string GenerateToken(string secret)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("id", Id),
                        new Claim(ClaimTypes.Email, Email)
                    }),
                    Expires = DateTime.Now.AddDays(30),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                        SecurityAlgorithms.HmacSha512Signature
                    )
                };

                return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            }
            catch { }
            return null!;
        }
    }
}
