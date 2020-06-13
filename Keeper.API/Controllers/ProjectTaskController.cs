using System.Threading.Tasks;
using Keeper.API.InputModels;
using Keeper.API.Models;
using Keeper.Data.Managers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.API.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProjectTaskController : BaseController
    {
        private IProjectTaskManager _projectTaskManager;

        public ProjectTaskController(IProjectTaskManager projectTaskManager)
        {
            _projectTaskManager = projectTaskManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var task = await _projectTaskManager.GetByIdAsync(id, CurrentUser.Id);

            if (task != null)
            {
                return Ok(new ResponseModel(new ProjectTaskModel(task)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to retrieve Task."));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync([FromBody] ProjectTaskInputModel input, int id)
        {
            var task = await _projectTaskManager.UpdateAsync(
                id,
                input.Name,
                input.Description,
                input.Hours,
                input.Date,                
                CurrentUser.Id
            );

            if (task != null)
            {
                return Ok(new ResponseModel(new ProjectTaskModel(task)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to update Task."));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _projectTaskManager.DeleteAsync(id, CurrentUser.Id);

            if (result)
            {
                return Ok(new ResponseModel("Task deleted."));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to delete Task."));
            }
        }

    }
}
