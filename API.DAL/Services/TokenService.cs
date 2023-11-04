using API.BLL.Entities.Identity;
using API.BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.DAL.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly SymmetricSecurityKey key;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"]));
        }
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.DisplayName)
            };
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokendiscrebtor = new SecurityTokenDescriptor
            {
                SigningCredentials = creds,
                Subject=new ClaimsIdentity(claims),
                Expires=DateTime.UtcNow.AddDays(10),
                Issuer = configuration["Token:Issuer"]
            };

            var tokenhandler=new JwtSecurityTokenHandler();
            var token=tokenhandler.CreateToken(tokendiscrebtor);
            return tokenhandler.WriteToken(token);
        }
    }
}
