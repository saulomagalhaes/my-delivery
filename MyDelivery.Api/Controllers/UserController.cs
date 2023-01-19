using Microsoft.AspNetCore.Mvc;
using MyDelivery.Application.DTOs.User;
using MyDelivery.Application.Services.Contracts;

namespace MyDelivery.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] UserDTO userDTO)
        {
            var result = await _userService.GenerateToken(userDTO);
            if(result.Sucess)
                return Ok(result.Data);
            if (result.Message == "Problema na validação")
                return BadRequest(new { errors = result.Errors });
            return NotFound(new { error = result.Message });
        }
    }
}
