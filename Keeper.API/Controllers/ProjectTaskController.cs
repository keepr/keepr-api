using System.Threading.Tasks;
using Keeper.API.InputModels;
using Keeper.API.Models;
using Keeper.Data.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.API.Controllers
{
    [Route("api/tasks")]
    public class ProjectTaskController : BaseController
    {
        private IProjectTaskManager _projectTaskManager;

        public ProjectTaskController(IProjectTaskManager projectTaskManager)
        {
            _projectTaskManager = projectTaskManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<ProjectTaskModel>>> GetByIdAsync(int id)
        {
            var task = await _projectTaskManager.GetByIdAsync(id, CurrentUser.Id);

            if (task != null)
            {
                return Ok(new ResponseModel<ProjectTaskModel>(new ProjectTaskModel(task)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to retrieve Task."));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<ProjectTaskModel>>> UpdateAsync([FromBody] ProjectTaskInputModel input, int id)
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
                return Ok(new ResponseModel<ProjectTaskModel>(new ProjectTaskModel(task)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to update Task."));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel<string>>> DeleteAsync(int id)
        {
            var result = await _projectTaskManager.DeleteAsync(id, CurrentUser.Id);

            if (result)
            {
                return Ok(new ResponseModel<string>("Task deleted."));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to delete Task."));
            }
        }
    }
}
