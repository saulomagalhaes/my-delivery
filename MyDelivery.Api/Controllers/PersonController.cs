using Microsoft.AspNetCore.Mvc;
using MyDelivery.Application.DTOs;
using MyDelivery.Application.DTOs.Person;
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
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        return BadRequest(result);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetById(int id)
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

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] PersonDTO personDTO)
    {
        var result = await _personService.Update(id, personDTO);
        if (result.Sucess)
            return NoContent();
        return BadRequest(result);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _personService.Delete(id);
        if(result.Sucess)
            return NoContent();
        return BadRequest(result);
    }
}
