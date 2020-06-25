using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keeper.API.InputModels;
using Keeper.API.Models;
using Keeper.Data.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.API.Controllers
{
    [Route("api/clients")]
    public class ClientController : BaseController
    {
        private IClientManager _clientManager;
        private IContactManager _contactManager;
        private IProjectManager _projectManager;

        public ClientController(
            IClientManager clientManager,
            IContactManager contactManager,
            IProjectManager projectManager
        )
        {
            _clientManager = clientManager;
            _contactManager = contactManager;
            _projectManager = projectManager;
        }

        [HttpGet("")]
        public async Task<ActionResult<ResponseModel<IEnumerable<ClientModel>>>> GetClientsByUserIdAsync()
        {
            var clients = await _clientManager.GetByUserIdAsync(CurrentUser.Id);

            if (clients != null)
            {
                return Ok(new ResponseModel<IEnumerable<ClientModel>>(clients.Select(x => new ClientModel(x))));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to retrieve Clients for current User."));
            }
        }

        [HttpPost("")]
        public async Task<ActionResult<ResponseModel<ClientModel>>> CreateAsync([FromBody] ClientInputModel input)
        {
            var client = await _clientManager.CreateAsync(input.Name, input.Address, CurrentUser.Id);

            if (client != null)
            {
                return Ok(new ResponseModel<ClientModel>(new ClientModel(client)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to create Client."));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<ClientModel>>> GetClientByIdAsync(int id)
        {
            var client = await _clientManager.GetByIdAsync(id, CurrentUser.Id);

            if (client != null)
            {
                return Ok(new ResponseModel<ClientModel>(new ClientModel(client)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to retrieve Client."));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<ClientModel>>> UpdateAsync([FromBody] ClientInputModel input, int id)
        {
            var client = await _clientManager.UpdateAsync(id, input.Name, input.Address, CurrentUser.Id);

            if (client != null)
            {
                return Ok(new ResponseModel<ClientModel>(new ClientModel(client)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to update Client."));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel<string>>> DeleteAsync(int id)
        {
            var result = await _clientManager.DeleteAsync(id, CurrentUser.Id);

            if (result)
            {
                return Ok(new ResponseModel<string>("Client deleted."));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to delete Client."));
            }
        }

        [HttpPost("{id}/contacts")]
        public async Task<ActionResult<ResponseModel<ContactModel>>> CreateClientContactAsync([FromBody] ContactInputModel inputModel, int id)
        {
            var contact = await _contactManager.CreateAsync(
                inputModel.FirstName, 
                inputModel.LastName,
                inputModel.Email,
                inputModel.Phone,
                id,
                CurrentUser.Id
            );

            if (contact != null)
            {
                return Ok(new ResponseModel<ContactModel>(new ContactModel(contact)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to create a Contact for this Client."));
            }
        }
       
        [HttpPost("{id}/projects")]
        public async Task<ActionResult<ResponseModel<ProjectModel>>> CreateClientProjectAsync([FromBody] ProjectInputModel inputModel, int id)
        {
            var project = await _projectManager.CreateAsync(
                inputModel.Name,
                inputModel.Budget,
                inputModel.Currency,
                inputModel.HourlyRate,
                id,
                CurrentUser.Id
            );

            if (project != null)
            {
                return Ok(new ResponseModel<ProjectModel>(new ProjectModel(project)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to create a Project for this Client."));
            }
        }
    }
}
