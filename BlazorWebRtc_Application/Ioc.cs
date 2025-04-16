using BlazorWebRtc_Application.Interface.Services;
using BlazorWebRtc_Application.Interface.Services.Manager;
using BlazorWebRtc_Application.Services;
using BlazorWebRtc_Application.Services.Manager;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWebRtc_Application;

public static class Ioc
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IConnectionManager, ConnectionManager>();

        services.AddHttpContextAccessor();

        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(Ioc).Assembly));
        return services;
    }
}