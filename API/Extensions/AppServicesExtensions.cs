using Core.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class AppServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {          
            services.AddScoped<IStockService, StockService>();
            return services;
        }
    }
}