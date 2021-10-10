using Blazor.Contacts.Wasm.Respositories;
using Blazor.Contacts.Wasm.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Contacts.Wasm.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRespository _contactRespository;

        public ContactsController(IContactRespository contactRespository)
        {
            _contactRespository = contactRespository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Contact contact)
        {
            if (contact == null)
                return BadRequest();

            if (string.IsNullOrEmpty(contact.FirstName))
                ModelState.AddModelError("FirstName", "First name can´t be empty empty");

            if (string.IsNullOrEmpty(contact.LastName))
                ModelState.AddModelError("LastName", "Last name can´t be empty empty");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _contactRespository.InsertContact(contact);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Contact contact)
        {
            if (contact == null)
                return BadRequest();

            if (string.IsNullOrEmpty(contact.FirstName))
                ModelState.AddModelError("FirstName", "First name can´t be empty empty");

            if (string.IsNullOrEmpty(contact.LastName))
                ModelState.AddModelError("LastName", "Last name can´t be empty empty");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _contactRespository.UpdateContact(contact);

            return NoContent();
        }

        [HttpGet]
        public async Task<IEnumerable<Contact>> Get()
        {
            return await _contactRespository.GetAllContacts();
        }

        [HttpGet("{id}")]
        public async Task<Contact> Get(int id)
        {
            return await _contactRespository.GetContactById(id);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _contactRespository.DeleteContact(id);
        }

    }
}
