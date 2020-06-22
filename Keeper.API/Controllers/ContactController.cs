using System.Threading.Tasks;
using Keeper.API.InputModels;
using Keeper.API.Models;
using Keeper.Data.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Keeper.API.Controllers
{
    [Route("api/contacts")]
    public class ContactController : BaseController
    {
        private IContactManager _contactManager;

        public ContactController(IContactManager contactManager)
        {
            _contactManager = contactManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<ContactModel>>> GetByIdAsync(int id)
        {
            var contact = await _contactManager.GetByIdAsync(id, CurrentUser.Id);

            if (contact != null)
            {
                return Ok(new ResponseModel<ContactModel>(new ContactModel(contact)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to retrieve Contact."));
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel<ContactModel>>> UpdateAsync([FromBody] ContactInputModel input, int id)
        {
            var contact = await _contactManager.UpdateAsync(
                id,
                input.FirstName,
                input.LastName,
                input.Email,
                input.Phone,
                CurrentUser.Id
            );

            if (contact != null)
            {
                return Ok(new ResponseModel<ContactModel>(new ContactModel(contact)));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to update Contact."));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel<string>>> DeleteAsync(int id)
        {
            var result = await _contactManager.DeleteAsync(id, CurrentUser.Id);

            if (result)
            {
                return Ok(new ResponseModel<string>("Contact deleted."));
            }
            else
            {
                return BadRequest(new ErrorModel("Unable to delete Contact."));
            }
        }
    }
}
