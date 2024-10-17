using Microsoft.OpenApi.Models;

namespace Store.Web.Extensions
{
    public static class SwaggerServiceExtension
    {
        public static IServiceCollection AddSwaggerDocumetation(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("V1", new OpenApiInfo
                {
                    Title = "Store App",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "John Walkner",
                        Email = "John.Walkner@email.com",
                        Url = new Uri("https://twitter.com/jwalkner"),
                    }
                });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Id= "bearer",
                        Type= ReferenceType.SecurityScheme
                    }
                };

                option.AddSecurityDefinition("bearer", securitySchema);

                var securityRequirments = new OpenApiSecurityRequirement
                {
                    {securitySchema , new[] { "bearer" } }
                };

                option.AddSecurityRequirement(securityRequirments);


            });

            return services;
        }
    }
}
