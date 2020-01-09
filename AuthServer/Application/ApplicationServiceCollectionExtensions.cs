using System.Reflection;
using AuthServer.Core.Services;
using AutoMapper;
using Common.Web.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AuthServer.Application
{
public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //config AutoMapper profiles
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        services.AddTransient<IUserManagerService, UserManagerService>();
        services.AddTransient<IRoleManagerService, RoleManagerService>();
        
        //config MediateR dependency injection
        services.AddMediatR(Assembly.GetExecutingAssembly());
        //config MediateR behaviours
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        
        return services;
    }
}
}