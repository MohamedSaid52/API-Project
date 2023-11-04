using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwagerDocumention(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("V1", new OpenApiInfo { Title = "TalabetDemo", Version = "V1" });
                var secrityschema = new OpenApiSecurityScheme
                {
                    Description = "Jwt Auth Bearer Schem",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                opt.AddSecurityDefinition("Bearer", secrityschema);
                var securityrequirement = new OpenApiSecurityRequirement
                {
                    {secrityschema,new[]{"Bearer"} }
                };
                opt.AddSecurityRequirement(securityrequirement);  
            });
            return services;
        }

    }
}
