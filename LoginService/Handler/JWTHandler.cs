using LoginService.Models;
using LoginService.Models.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LoginService.Handler
{
    public class JWTHandler
    {
        private IConfiguration _config;
        DatabaseContext db;

        public JWTHandler(IConfiguration config)
        {
            _config = config;
            db = new DatabaseContext();
        }

        public async Task<string> GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var signCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims: new List<Claim>(), // claims (are used to filter the data)
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserViewModel> AuthenticateUser(UserDetail login)
        {
            UserViewModel user = null;

            var userDetails = db.UserDetails.AsAsyncEnumerable();
            await foreach (var x in userDetails)
            {
                if (x.UserName == login.UserName && x.Password == login.Password)
                {
                    var userRole = await db.UserRoles.FindAsync(x.RoleId);
                    return new UserViewModel { UserName = login.UserName, Role = userRole.Role };
                }
            }
            return user;
        }
    }
}