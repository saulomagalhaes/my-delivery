using Microsoft.AspNetCore.Mvc;
using MyDelivery.Application.DTOs;
using MyDelivery.Application.Services.Contracts;

namespace MyDelivery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PersonDTO personDTO)
        {
            var result = await _personService.Create(personDTO);
            if(result.Sucess)
                return Created("", result.Data);
            return BadRequest(result.Errors);
        }
    }
}
