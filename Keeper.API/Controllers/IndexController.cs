using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Keeper.API.InputModels;
using Keeper.API.Models;
using Keeper.Data.Managers;
using Keeper.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Keeper.API.Controllers
{
    [Route("/")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        private IConfiguration _config;
        private IUserManager _userManager;

        public IndexController(IConfiguration config, IUserManager userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Keeper API is running!");
        }

        [HttpPost("api/login")]
        public async Task<ActionResult> LoginAsync([FromBody] LoginInputModel input)
        {
            var user = await _userManager.GetUserByLoginAsync(input.Email, input.Password);

            if (user != null)
            {
                var jwt = GenerateJWT(user.Id, user.Email);
                return Ok(new ResponseModel(jwt));
            }
            else
            {
                return BadRequest(new ErrorModel("Invalid email / password."));
            }
        }

        [HttpPost("api/register")]
        public async Task<ActionResult> RegisterAsync([FromBody] RegisterInputModel input)
        {
            var user = await _userManager.CreateUserAsync(input.FirstName, input.LastName, input.Email, input.Password);

            if (user != null)
            {
                return Ok(new ResponseModel(user.Token));
            }
            else
            {
                return BadRequest(new ErrorModel("User account with email address already exists."));
            }
        }

        [HttpPost("api/activate/{token}")]
        public async Task<ActionResult> ActivateAsync(string token)
        {
            var user = await _userManager.ActivateUserAsync(token);

            if (user != null)
            {
                return Ok(new ResponseModel(GenerateJWT(user.Id, user.Email)));
            }
            else
            {
                return BadRequest(new ErrorModel("Invalid token."));
            }
        }

        [HttpPost("api/reset-password")]
        public async Task<ActionResult> ResetPasswordAsync([FromBody] LoginInputModel input)
        {
            var token = await _userManager.ResetPasswordAsync(input.Email);

            if (token != null)
            {
                return Ok(new ResponseModel(token));
            }
            else
            {
                return BadRequest(new ErrorModel("No user account with email address."));
            }
        }

        [HttpPost("api/update-password/{token}")]
        public async Task<ActionResult> ResetPasswordAsync([FromBody] LoginInputModel input, string token)
        {
            var result = await _userManager.UpdatePasswordAsync(input.Password, token);

            if (result)
            {
                return Ok(new ResponseModel("Password updated."));
            }
            else
            {
                return BadRequest(new ErrorModel("Invalid token."));
            }
        }

        public string GenerateJWT(int id, string email)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenSecret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // token props
            var issuer = _config["TokenIssuer"];
            var audience = _config["TokenIssuer"];
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.UserData, id.ToString()),
                new Claim(ClaimTypes.Email, email),
            };
            var expires = DateTime.UtcNow.AddDays(30);

            // finally create token
            var token = new JwtSecurityToken(issuer, audience, claims, null, expires, creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
