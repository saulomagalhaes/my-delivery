using Microsoft.AspNetCore.Mvc;
using MyDelivery.Application.DTOs.Purchase;
using MyDelivery.Application.Services;
using MyDelivery.Application.Services.Contracts;
using MyDelivery.Domain.Validations;

namespace MyDelivery.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PurchaseController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;

    public PurchaseController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] PurchaseDTO purchaseDTO)
    {
        try
        {
            var result = await _purchaseService.Create(purchaseDTO);
            if (result.Sucess)
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result.Data);
            return BadRequest(new { errors = result.Errors });
        }
        catch (DomainValidationException ex)
        {
            var result = ResultService.Fail(ex.Message);
            return NotFound(new { error = result.Message });
        }
    }

    [HttpGet]
    public async Task<ActionResult> GetPurchases()
    {
        var result = await _purchaseService.GetPurchases();
        if (result.Sucess)
            return Ok(result.Data);
        return BadRequest(result);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var result = await _purchaseService.GetById(id);
        if (result.Sucess)
            return Ok(result.Data);
        return NotFound(new { error = result.Message });
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] PurchaseDTO purchaseDTO)
    {
        try
        {
            var result = await _purchaseService.Update(id, purchaseDTO);
            if (result.Sucess)
                return NoContent();
            if (result.Message == "Problema na validação")
                return BadRequest(new { errors = result.Errors });
            return NotFound(new { errors = result.Message });
        }
        catch (DomainValidationException ex)
        {
            var result = ResultService.Fail(ex.Message);
            return NotFound(new { error = result.Message });
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _purchaseService.Delete(id);
        if (result.Sucess)
            return NoContent();
        return NotFound(new { error = result.Message });
    }
}
