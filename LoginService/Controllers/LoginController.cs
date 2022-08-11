using LoginService.Handler;
using LoginService.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserDetail login)
        {
            IActionResult response = Unauthorized();
            JWTHandler jWTHandler = new JWTHandler(_config);

            var user = await jWTHandler.AuthenticateUser(login);

            if (user != null)
            {
                user.Token = await jWTHandler.GenerateJSONWebToken();
                response = Ok(user);
            }

            return response;
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> GetDetails(UserDetail login)
        {
            return Ok(login);
        }
    }
}