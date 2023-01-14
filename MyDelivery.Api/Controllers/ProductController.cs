using Microsoft.AspNetCore.Mvc;
using MyDelivery.Application.DTOs.Product;
using MyDelivery.Application.Services.Contracts;

namespace MyDelivery.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ProductDTO productDTO)
    {
        var result = await _productService.Create(productDTO);
        if (result.Sucess)
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result.Data);
        return BadRequest(new { errors = result.Errors });
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var result = await _productService.GetById(id);
        if (result.Sucess)
            return Ok(result.Data);
        return NotFound(new { error = result.Message });
    }

    [HttpGet]
    public async Task<ActionResult> GetProducts()
    {
        var result = await _productService.GetProducts();
        if (result.Sucess)
            return Ok(result.Data);
        return BadRequest(new { errors = result.Errors });
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] ProductDTO productDTO)
    {
        var result = await _productService.Update(id, productDTO);
        if (result.Sucess)
            return NoContent();
        if (result.Message == "Problema na validação")
            return BadRequest(new { errors = result.Errors });
        return NotFound(new { errors = result.Message });
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _productService.Delete(id);
        if (result.Sucess)
            return NoContent();
        return NotFound(new { error = result.Message });
    }
}
