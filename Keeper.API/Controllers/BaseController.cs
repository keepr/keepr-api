using System;
using System.Linq;
using Keeper.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Keeper.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController: Controller
    {
        public UserModel CurrentUser;

        public BaseController() {}

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext?.User?.Claims != null)
            {
                // get user id and email from claim
                var claims = HttpContext.User.Claims.ToArray();
                CurrentUser = new UserModel()
                {
                    Id = Int32.Parse(claims[0].Value),
                    Email = claims[1].Value
                };
            }
        }
    }
}
