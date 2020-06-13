using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keeper.API.InputModels;
using Keeper.API.Models;
using Keeper.Data.Managers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProjectController : BaseController
    {
        private IProjectManager _projectManager;
        private IProjectTaskManager _projectTaskManager;

        public ProjectController(IProjectManager projectManager, IProjectTaskManager projectTaskManager)
        {
            _projectManager = projectManager;
            _projectTaskManager = projectTaskManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var project = await _projectManager.GetByIdAsync(id, CurrentUser.Id);

            if (project != null)
            {
                return Ok(new ResponseModel(new ProjectModel(project)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to retrieve Project."));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync([FromBody] ProjectInputModel input, int id)
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
                return Ok(new ResponseModel(new ProjectModel(project)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to update Project."));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _projectManager.DeleteAsync(id, CurrentUser.Id);

            if (result)
            {
                return Ok(new ResponseModel("Project deleted."));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to delete Project."));
            }
        }

        
        [HttpPost("{id}/archive")]
        public async Task<ActionResult> ArchiveAsync(int id)
        {
            var result = await _projectManager.ToggleArchiveAsync(id, CurrentUser.Id);

            if (result)
            {
                return Ok(new ResponseModel("Project archived."));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to archive Project."));
            }
        }

        [HttpGet("{id}/tasks")]
        public async Task<ActionResult> GetProjectTasksAsync(int id)
        {
            var tasks = await _projectTaskManager.GetByProjectIdAsync(id, CurrentUser.Id);

            if (tasks != null)
            {
                return Ok(new ResponseModel(tasks.Select(x => new ProjectTaskModel(x))));
            }
            else 
            {
                return BadRequest(new ErrorModel("Unable to retrieve Project Tasks."));
            }
        }

        [HttpPost("{id}/tasks")]
        public async Task<ActionResult> CreateProjectTaskAsync([FromBody] ProjectTaskInputModel input, int id)
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
                return Ok(new ResponseModel(new ProjectTaskModel(task)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to create a Task for this Project."));
            }
        }
    }
}
