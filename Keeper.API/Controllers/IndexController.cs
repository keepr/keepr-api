using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.API.Controllers
{
    [Route("/")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Keeper API is running!");
        }
    }
}
