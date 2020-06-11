using Keeper.API.InputModels;
using Keeper.API.Models;
using Keeper.Data.Managers;
using Keeper.Data.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keeper.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController: BaseController
    {
        private IConfiguration _config;
        private IUserManager _userManager;

        public UserController(IConfiguration config, IUserManager userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        /// <summary>
        /// Get current user from JWT
        /// </summary>
        /// <returns>User</returns>
        [HttpGet("me")]
        public async Task<ActionResult> MeAsync()
        {
            var user = await _userManager.GetUserByIdAsync(CurrentUser.Id);
            if (user != null)
            {
                return Ok(new ResponseModel(new UserModel(user)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to retrieve current user."));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input">UserUpdateInputModel</param>
        /// <returns>User</returns>]
        [HttpPost("me/update")]
        public async Task<ActionResult> UpdateAsync([FromBody] UserUpdateInputModel input)
        {
            var user = await _userManager.UpdateUserAsync(
                CurrentUser.Id,
                input.FirstName,
                input.LastName,
                input.Email,
                input.Address,
                input.Password,
                input.Currency,
                input.HourlyRate
            );

            if (user != null)
            {
                return Ok(new ResponseModel(new UserModel(user)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to update user."));
            }
        }
    }
}
