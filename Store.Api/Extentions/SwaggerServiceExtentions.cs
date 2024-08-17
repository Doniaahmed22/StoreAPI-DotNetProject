using Microsoft.OpenApi.Models;

namespace Store.Api.Extentions
{
    public static class SwaggerServiceExtentions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "StoreApi", Version = "v1" });

                var securitySchceme = new OpenApiSecurityScheme
                {
                    Description = " jwt",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "bearer"
                    }
                };
                options.AddSecurityDefinition("bearer", securitySchceme);


                var securityRequirements = new OpenApiSecurityRequirement
                {
                    { securitySchceme , new[]{ "bearer"} }
                };
                options.AddSecurityRequirement(securityRequirements);
            });
            return services;
        }
    }
}
