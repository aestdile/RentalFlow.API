using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalFlow.API.Application.Interfaces.IServices;
using RentalFlow.API.Application.Interfaces.Repositories;
using RentalFlow.API.Application.Services;
using RentalFlow.API.Application.Validators.GuestDTOsValidators;
using RentalFlow.API.Application.Validators.HomeDTOsValidators;
using RentalFlow.API.Application.Validators.HomeRequestDTOsValidators;
using RentalFlow.API.Application.Validators.HostDTOsValidators;
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

        services.AddFluentValidationAutoValidation();

        services.AddValidatorsFromAssemblyContaining<GuestCreateDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<GuestDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<GuestUpdateDtoValidator>();

        services.AddValidatorsFromAssemblyContaining<HomeCreateDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<HomeDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<HomeUpdateDtoValidator>();

        services.AddValidatorsFromAssemblyContaining<HomeRequestCreateDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<HomeRequestDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<HomeRequestUpdateDtoValidator>();

        services.AddValidatorsFromAssemblyContaining<HostCreateDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<HostDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<HostUpdateDtoValidator>();

        return services;
    }
}
