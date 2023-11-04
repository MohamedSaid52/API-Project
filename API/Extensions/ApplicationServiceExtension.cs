using API.BLL.Interfaces;
using API.BLL.Interfaces___Copy;
using API.DAL;
using API.DAL.Services;
using API.Errors;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IProductReposatory, ProductReposatory>();
            services.AddScoped(typeof(IGenricReposatory<>), typeof(GenricReposatory<>));
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddScoped<IBasketReposatory , BasketReposatory>();
            services.AddScoped<ITokenService, TokenService> ();
            services.AddScoped<IOrederService, OrederService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAppSeesion, AppSeesion>();
            services.AddScoped<IPaymentService,PaymentService >();
            services.AddSingleton<IResponseCashService, ResponseCashService>();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ActionContext =>
                {
                    var errors=ActionContext.ModelState.Where(e=>e.Value.Errors.Count>0)
                             .SelectMany(x=>x.Value.Errors)
                             .Select(x=>x.ErrorMessage).ToList();
                    var errorResponse = new APIValidationErrorResponse
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

