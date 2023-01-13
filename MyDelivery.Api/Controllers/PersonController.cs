using Microsoft.AspNetCore.Mvc;
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
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result.Data);
        return BadRequest(new { errors = result.Errors });
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var result = await _personService.GetById(id);
        if(result.Sucess)
            return Ok(result.Data);
        return NotFound(new { error = result.Message });
    }

    [HttpGet]
    public async Task<ActionResult> GetPeople()
    {
        var result = await _personService.GetPeople();
        if(result.Sucess)
            return Ok(result.Data);
        return BadRequest(new { errors = result.Errors });
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] PersonDTO personDTO)
    {
        var result = await _personService.Update(id, personDTO);
        if (result.Sucess)
            return NoContent();
        if(result.Message == "Problema na validação")
            return BadRequest(new { errors = result.Errors }); 
        return NotFound(new { errors = result.Message });
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _personService.Delete(id);
        if(result.Sucess)
            return NoContent();
        return NotFound(new { error = result.Message });
    }
}
