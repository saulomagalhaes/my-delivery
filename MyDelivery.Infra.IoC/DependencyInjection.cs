using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyDelivery.Application.Profiles;
using MyDelivery.Application.Services;
using MyDelivery.Application.Services.Contracts;
using MyDelivery.Domain.Authentication;
using MyDelivery.Domain.Contracts.Repositories;
using MyDelivery.Infra.Data.Authentication;
using MyDelivery.Infra.Data.Context;
using MyDelivery.Infra.Data.Repositories;

namespace MyDelivery.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MyDeliveryConnection");
        services.AddEntityFrameworkSqlServer()
                .AddDbContext<MyDeliveryDbContext>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IPurchaseRepository, PurchaseRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(PersonProfile));
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IPurchaseService, PurchaseService>();
        services.AddScoped<IUserService, UserService>();
        return services;
    }

}
