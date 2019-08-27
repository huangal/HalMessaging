using System;

using HalMessaging.Contracts;
using HalMessaging.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HalMessaging.Bindings
{
    public static class Binding
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMessageService, MessageService>();
            //  services.AddSingleton<IIdentityManagmentConfig>(
            //cw => configuration.GetSection("IdentityManagmentConfig").Get<IdentityManagmentConfig>());

            services.Configure<ForecastConfiguration>(configuration.GetSection("Features"));


            return services;
        }
    }
}
