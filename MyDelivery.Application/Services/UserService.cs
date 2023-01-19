using MyDelivery.Application.DTOs.User;
using MyDelivery.Application.DTOs.Validations;
using MyDelivery.Application.Services.Contracts;
using MyDelivery.Domain.Authentication;
using MyDelivery.Domain.Contracts.Repositories;

namespace MyDelivery.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenGenerator _tokenGenerator;

    public UserService(IUserRepository userRepository, ITokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ResultService<dynamic>> GenerateToken(UserDTO userDTO)
    {
        var result = new UserDTOValidator().Validate(userDTO);
        if (!result.IsValid)
            return ResultService.RequestError<dynamic>("Problema na validação", result);

        var user = await _userRepository.GetUserByEmailAndPassword(userDTO.Email, userDTO.Password);
        if (user == null)
            return ResultService.Fail<dynamic>("Usuário ou senha não encontrado");

        return ResultService.Ok(_tokenGenerator.Generator(user));
    }
}
