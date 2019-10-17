using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Cors;

namespace CurrencyConverter.Configurations
{
    public static class CorsProvider
    {
        public static IServiceCollection ProvideCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("FrontView", builder =>
                {
                    builder.WithMethods(new[] {"POST"});
                    builder.AllowAnyHeader();
                    builder.AllowCredentials();
                    builder.WithOrigins(new[] {"http://localhost:4200"});
                });
            });
            return services;
        }
    }
}