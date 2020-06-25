using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keeper.API.InputModels;
using Keeper.API.Models;
using Keeper.Data.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.API.Controllers
{
    [Route("api/projects")]
    public class ProjectController : BaseController
    {
        private IProjectManager _projectManager;
        private IProjectTaskManager _projectTaskManager;

        public ProjectController(IProjectManager projectManager, IProjectTaskManager projectTaskManager)
        {
            _projectManager = projectManager;
            _projectTaskManager = projectTaskManager;
        }

        [HttpGet("")]
        public async Task<ActionResult<ResponseModel<IEnumerable<ProjectModel>>>> GetProjectsByClientIdAsync(int id)
        {
            var projects = await _projectManager.GetByUserIdAsync(CurrentUser.Id);

            if (projects != null)
            {
                return Ok(new ResponseModel<IEnumerable<ProjectModel>>(projects.Select(x => new ProjectModel(x))));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to retrieve Projects for User."));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<ProjectModel>>> GetByIdAsync(int id)
        {
            var project = await _projectManager.GetByIdAsync(id, CurrentUser.Id);

            if (project != null)
            {
                return Ok(new ResponseModel<ProjectModel>(new ProjectModel(project)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to retrieve Project."));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<ProjectModel>>> UpdateAsync([FromBody] ProjectInputModel input, int id)
        {
            var project = await _projectManager.UpdateAsync(
                id,
                input.Name,
                input.Budget,
                input.Currency,
                input.HourlyRate,
                CurrentUser.Id                
            );

            if (project != null)
            {
                return Ok(new ResponseModel<ProjectModel>(new ProjectModel(project)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to update Project."));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel<string>>> DeleteAsync(int id)
        {
            var result = await _projectManager.DeleteAsync(id, CurrentUser.Id);

            if (result)
            {
                return Ok(new ResponseModel<string>("Project deleted."));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to delete Project."));
            }
        }

        
        [HttpPost("{id}/archive")]
        public async Task<ActionResult<ResponseModel<string>>> ArchiveAsync(int id)
        {
            var result = await _projectManager.ToggleArchiveAsync(id, CurrentUser.Id);

            if (result)
            {
                return Ok(new ResponseModel<string>("Project archived."));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to archive Project."));
            }
        }

        [HttpGet("{id}/tasks")]
        public async Task<ActionResult<ResponseModel<IEnumerable<ProjectTaskModel>>>> GetProjectTasksAsync(int id)
        {
            var tasks = await _projectTaskManager.GetByProjectIdAsync(id, CurrentUser.Id);

            if (tasks != null)
            {
                return Ok(new ResponseModel<IEnumerable<ProjectTaskModel>>(tasks.Select(x => new ProjectTaskModel(x))));
            }
            else 
            {
                return BadRequest(new ErrorModel("Unable to retrieve Project Tasks."));
            }
        }

        [HttpPost("{id}/tasks")]
        public async Task<ActionResult<ResponseModel<ProjectTaskModel>>> CreateProjectTaskAsync([FromBody] ProjectTaskInputModel input, int id)
        {
            var task = await _projectTaskManager.CreateAsync(
                input.Name,
                input.Description,
                input.Hours,
                input.Date,
                id,
                CurrentUser.Id
            );

            if (task != null)
            {
                return Ok(new ResponseModel<ProjectTaskModel>(new ProjectTaskModel(task)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to create a Task for this Project."));
            }
        }
    }
}
