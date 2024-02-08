using Microsoft.IdentityModel.Tokens;
using StockApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockApi.Services
{
    public class JWTServices
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _jwtKey;
        public JWTServices(IConfiguration configuration)
        {
            _configuration = configuration;
            _configuration = configuration;
            _jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
        }
        public string CreateJWT(StockUser user)
        {
            var userClient = new List<Claim> 
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Surname,user.LastName),
            };
            var creadential = new SigningCredentials(_jwtKey, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userClient),
                Expires = DateTime.UtcNow.AddDays(int.Parse(_configuration["JWT:ExpireInDays"]!)),
                SigningCredentials = creadential,
                Issuer = _configuration["JWT:Issuer"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var jwt = tokenHandler.CreateToken(tokenDescription);
                return tokenHandler.WriteToken(jwt);
            }catch (Exception ex) { return ex.Message; }
           
        }
    }
}
