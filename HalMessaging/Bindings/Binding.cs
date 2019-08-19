using System;
using HalMessaging.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HalMessaging.Bindings
{
    public static class Binding
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IMessageService, MessageService>();

            return services;
        }
    }
}
