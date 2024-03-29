﻿using System;
using System.Collections.Generic;
using System.Linq;
using HalMessaging.Contracts;
using HalMessaging.Models;
using HalMessaging.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HalMessaging.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    //[ApiConventionType(typeof(HalApiConventions))]
    [FormatFilter]
    public class ValuesController : ControllerBase
    {
        private readonly ForecastConfiguration _featuresConfiguration;
        private readonly IMessageService _messageService;

        public ValuesController(IMessageService messageService, IOptions<ForecastConfiguration> options )
        {
            _messageService = messageService;
            _featuresConfiguration = options.Value;
        }



        /// <summary>
        /// Get List of Messages
        /// </summary>
        /// <returns>Collection of Messages</returns
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Message>))]
        [HttpGet]
        public IEnumerable<Message> Get()
        {
            List<Message> messages = _messageService.GetMessages();

            if (messages != null && messages.Any())
                return messages;
            return (IEnumerable<Message>)NotFound();

            //    return Ok(messages);
            //return NotFound();

        }

        //[ApiConventionMethod(typeof(HalApiConventions), nameof(HalApiConventions.Get))]
        [HttpGet("data")]
        public IEnumerable<Message> GetData()
        {
            List<Message> messages = _messageService.GetMessages();

            if (messages != null && messages.Any())
                return messages;
            return (IEnumerable<Message>)NotFound();

            //    return Ok(messages);
            //return NotFound();

        }



        /// <summary>
        ///  Get Message by Id
        /// </summary>
        /// <param name="id">Message Id</param>
        /// <returns>Message object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Message))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            var message = _messageService.GetMessage(id);

            if (message != null )
                return Ok(message);
            return NotFound();
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            Guid newGuid;

            if (Guid.TryParse(value, out newGuid))
            {
                return Ok(newGuid.ToString());
            }
            else return BadRequest(value);
                                 
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
