using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using VehicleAPI.ViewModels.Authentication;

namespace VehicleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;
        public AuthenticationController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {

            var userExsists = await _userManager.FindByNameAsync(registerModel.UserName);
            if (userExsists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthenticationResponse
                {
                    Status = "Failed",
                    Message = "User already exists"
                });

            IdentityUser identityUser = new IdentityUser()
            {
                Email = registerModel.Email,
                UserName = registerModel.UserName
            };

            var result = await _userManager.CreateAsync(identityUser, registerModel.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new AuthenticationResponse
                {
                    Status = "Failed",
                    Message = "User creation failed"
                });

            return Ok(new AuthenticationResponse
            {
                Status = "Success",
                Message = $"User {registerModel.UserName} is created successfully"

            });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("token")]
        public string Token()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)

                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
