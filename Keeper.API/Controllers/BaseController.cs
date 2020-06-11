using Keeper.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keeper.API.Controllers
{
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
