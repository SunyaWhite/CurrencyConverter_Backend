using System;
using Microsoft.Extensions.DependencyInjection;
using CurrencyConverter.Core.Parsers;

namespace CurrencyConverter.Configurations
{
    public static class ParsersConfigurations
    {
        public static IServiceCollection ProvideParsers(this IServiceCollection services)
        {
            services.AddTransient<ECBParser>();
            services.AddTransient<RCBParser>();
            return services;
        }
    }
}