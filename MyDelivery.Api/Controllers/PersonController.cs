using Microsoft.AspNetCore.Mvc;
using MyDelivery.Application.DTOs;
using MyDelivery.Application.Services.Contracts;

namespace MyDelivery.Api.Controllers;

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
            return Created("", result);
        return BadRequest(result);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetById([FromRoute] int id)
    {
        var result = await _personService.GetById(id);
        if(result.Sucess)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet]
    public async Task<ActionResult> GetPeople()
    {
        var result = await _personService.GetPeople();
        if(result.Sucess)
            return Ok(result);
        return BadRequest(result);
    }
}
