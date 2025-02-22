using DotnetPractice.BuisinessLogic.Services.Interfaces;
using DotnetPractice.Common.Dtos.Auth;
using DotnetPractice.Common.Utils;
using DotnetPractice.DataAccess.Models;
using DotnetPractice.DataAccess.Repos.Interfaces;
using Microsoft.AspNetCore.Identity;


using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using DotnetPractice.DataAccess;
using DotnetPractice.DataAccess.Repos;

namespace DotnetPractice.BuisinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly TokenSettings _tokenSettings;
        private readonly UserManager<User> _userManager;
        public AuthService(TokenSettings tokenSettings, PostgresDbContext postgresDbContext,UserManager<User> userManager) {
            _tokenSettings = tokenSettings;
            _userManager = userManager;

        }
        public async Task<TokenResponseDto> GenerateNewTokenAsync(LoginDto loginInfo)
        {
            var user = await _userManager.FindByNameAsync(loginInfo.UserName);
            Console.WriteLine("Here is the user:");
            Console.WriteLine(loginInfo.UserName);
            Console.WriteLine(user);

            if (user != null && await _userManager.CheckPasswordAsync(user,loginInfo.Password))
            {

                var accessToken = await this.CreateAccessTokenAsync(user);

                return new TokenResponseDto()
                {
                    AccessToken = accessToken,
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                };
            }
            else
            {
                throw new Exception("Unauthorized");
            }
        }


        private async Task<string> CreateAccessTokenAsync(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenSettings.Secret));
            var tokenHandler = new JwtSecurityTokenHandler();
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            double tokenExpireTime = Convert.ToDouble(_tokenSettings.ExpireTime);
            var tokenDes = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _tokenSettings.Site,
                Audience = _tokenSettings.Audience,
                Expires = DateTime.Now.AddMinutes(tokenExpireTime),
                SigningCredentials = creds
            };

            var newAccessToken = tokenHandler.CreateToken(tokenDes);
            var encodedAccessToken = tokenHandler.WriteToken(newAccessToken);

            return encodedAccessToken;
        }
    }
}
