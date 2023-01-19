using MyDelivery.Application.DTOs.User;

namespace MyDelivery.Application.Services.Contracts;

public interface IUserService
{
    Task<ResultService<dynamic>> GenerateToken(UserDTO userDTO);
}
