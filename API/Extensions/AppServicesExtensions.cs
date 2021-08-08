using System.Linq;
using API.ErrorHandling;
using Core.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class AppServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {          
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IModalityService, ModalityService>();
            services.AddScoped<ISegmentService, SegmentService>();
            services.AddScoped<ITypeOfStockService, TypeOfStockService>();
            services.AddScoped<ISurtaxService, SurtaxService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAnnualReviewService, AnnualReviewService>();

             services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext => 
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();
                    
                    var errorResponse = new ServerValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}