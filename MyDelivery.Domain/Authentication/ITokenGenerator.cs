using MyDelivery.Domain.Entities;

namespace MyDelivery.Domain.Authentication;

public interface ITokenGenerator
{
    dynamic Generator(User user);
}
