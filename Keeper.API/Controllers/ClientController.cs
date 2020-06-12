using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keeper.API.InputModels;
using Keeper.API.Models;
using Keeper.Data.Managers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Keeper.API.Controllers
{
    [Route("api/client")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public async Task<ActionResult> GetClientsByUserIdAsync()
        {
            var clients = await _clientManager.GetByUserIdAsync(CurrentUser.Id);

            if (clients != null)
            {
                return Ok(new ResponseModel(clients.Select(x => new ClientModel(x))));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to retrieve Clients for current User."));
            }
        }

        [HttpPost("")]
        public async Task<ActionResult> CreateAsync([FromBody] ClientInputModel input)
        {
            var client = await _clientManager.CreateAsync(input.Name, input.Address, CurrentUser.Id);

            if (client != null)
            {
                return Ok(new ResponseModel(new ClientModel(client)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to create Client."));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetClientByIdAsync(int id)
        {
            var client = await _clientManager.GetByIdAsync(id, CurrentUser.Id);

            if (client != null)
            {
                return Ok(new ResponseModel(new ClientModel(client)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to retrieve Client."));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync([FromBody] ClientInputModel input, int id)
        {
            var client = await _clientManager.UpdateAsync(id, input.Name, input.Address, CurrentUser.Id);

            if (client != null)
            {
                return Ok(new ResponseModel(new ClientModel(client)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to update Client."));
            }
        }

        [HttpGet("{id}/contacts")]
        public async Task<ActionResult> GetContactsByClientIdAsync(int id)
        {
            var contacts = await _contactManager.GetByClientIdAsync(id, CurrentUser.Id);

            if (contacts != null)
            {
                return Ok(new ResponseModel(contacts.Select(x => new ContactModel(x))));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to retrieve Contacts for Client."));
            }
        }

        [HttpGet("{id}/projects")]
        public async Task<ActionResult> GetProjectsByClientIdAsync(int id)
        {
            var projects = await _projectManager.GetByClientIdAsync(id, CurrentUser.Id);

            if (projects != null)
            {
                return Ok(new ResponseModel(projects.Select(x => new ProjectModel(x))));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to retrieve Projects for Client."));
            }
        }
    }
}
