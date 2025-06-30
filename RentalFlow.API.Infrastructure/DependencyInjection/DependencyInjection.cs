using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalFlow.API.Application.Interfaces.IServices;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Application.Services;
using RentalFlow.API.Domain.Entities;
using RentalFlow.API.Infrastructure.Persistance;
using RentalFlow.API.Infrastructure.Repositories;

namespace RentalFlow.API.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RentalFlowDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("RentalFlowDbContext")));

        services.AddScoped<IGenericRepository<Host>, HostRepository>();
        services.AddScoped<IGenericRepository<Home>, HomeRepository>();
        services.AddScoped<IGenericRepository<HomeRequest>, HomeRequestRepository>();
        services.AddScoped<IGenericRepository<Guest>, GuestRepository>();

        services.AddScoped<IGuestService, GuestService>();
        services.AddScoped<IHostService, HostService>();
        services.AddScoped<IHomeRequestService, HomeRequestService>();
        services.AddScoped<IHomeService, HomeService>();

        return services;
    }
}
