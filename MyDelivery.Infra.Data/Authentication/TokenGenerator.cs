using Microsoft.IdentityModel.Tokens;
using MyDelivery.Domain.Authentication;
using MyDelivery.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyDelivery.Infra.Data.Authentication;

public class TokenGenerator : ITokenGenerator
{
    public dynamic Generator(User user)
    {
        var claims = new List<Claim>
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("Email", user.Email)
        };

        var expires = DateTime.Now.AddDays(1);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mydelivery"));
        var tokenData = new JwtSecurityToken(
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            expires: expires,
            claims: claims
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenData);
        return new
        {
            acess_token = token,
            expiration = expires
        };
    }
}
