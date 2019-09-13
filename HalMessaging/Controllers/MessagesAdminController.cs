using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HalMessaging.Attributes;
using HalMessaging.Contracts;
using HalMessaging.Extensions;
using HalMessaging.Security;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HalMessaging.Controllers
{
   [ApiVersion("0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MessagesAdminController : ControllerBase
    {
        private readonly IDataGenerator<Contact> _contactsGeneratorService;
        public MessagesAdminController(IDataGenerator<Contact> dataGeneratorService)
        {
            _contactsGeneratorService = dataGeneratorService;
        }

        [HttpGet("guid/{value}")]

        public IActionResult GetGuid(string value)
        {
            value =   Guid.NewGuid().ToString();
            Guid guid = Guid.Parse(value);

            return Ok(guid.ToString());
        }


        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        [HttpGet("Encrypt/{value}")]
        public IActionResult EncryptValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return BadRequest("Invalide Data");


            string encrypted = value.Encrypt();

            return Ok(encrypted.Base64UrlEncode());

        }

        [HttpPost("Encrypt")]
        public IActionResult Encrypt([FromBody]string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return BadRequest("Invalide Data");


            string encrypted = value.Encrypt();

            return Ok(encrypted.Base64UrlEncode());
                     
        }

        [HttpGet("Decrypt/{value}")]
        public IActionResult DecryptValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return BadRequest("Invalide Data");


            string decoded = value.Base64UrlDecode();

            return Ok(decoded.Decrypt());

        }


        [HttpPost("Decrypt")]
        public IActionResult Decrypt([FromBody]string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return BadRequest("Invalide Data");


            string decoded = value.Base64UrlDecode();

            return Ok(decoded.Decrypt());

        }


        [HttpGet("Contacts")]
        public IActionResult GetContacts()
        {

            return Ok(_contactsGeneratorService.Collection(11));
        }



    }
}
