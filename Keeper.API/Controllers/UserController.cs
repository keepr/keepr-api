using System.Threading.Tasks;
using Keeper.API.InputModels;
using Keeper.API.Models;
using Keeper.Data.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.API.Controllers
{
    [Route("api/users")]
    public class UserController: BaseController
    {
        private IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("me")]
        public async Task<ActionResult<ResponseModel<UserModel>>> MeAsync()
        {
            var user = await _userManager.GetByIdAsync(CurrentUser.Id);
            if (user != null)
            {
                return Ok(new ResponseModel<UserModel>(new UserModel(user)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to retrieve current User."));
            }
        }

        [HttpPut("me")]
        public async Task<ActionResult<ResponseModel<UserModel>>> UpdateAsync([FromBody] UserUpdateInputModel input)
        {
            var user = await _userManager.UpdateAsync(
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
                return Ok(new ResponseModel<UserModel>(new UserModel(user)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to update User."));
            }
        }

        [HttpDelete("me")]
        public async Task<ActionResult<ResponseModel<string>>> DeleteAsync()
        {
            var success = await _userManager.DeleteByIdAsync(CurrentUser.Id);

            if (success)
            {
                return Ok(new ResponseModel<string>("User deleted."));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to delete User."));
            }
        }
    }
}
