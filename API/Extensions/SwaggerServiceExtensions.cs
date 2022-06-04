using Microsoft.OpenApi.Models;

namespace API.Extensions;

public static class SwaggerServiceExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c => {
            var securitySchema = new OpenApiSecurityScheme
            {
                Description = "JWT Auth Bearer Scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            c.AddSecurityDefinition("Bearer", securitySchema);
            var securityRequirements = new OpenApiSecurityRequirement{{securitySchema, new []{
                "Bearer"
            }}};
            c.AddSecurityRequirement(securityRequirements);
        });
        return services;
    }

    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {    
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }
}