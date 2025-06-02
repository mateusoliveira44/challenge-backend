using Microsoft.OpenApi.Models;
using System.Reflection;

namespace challenge_backend.API.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "Desafio",
                    Description = "Desafio API Swagger",
                    Contact = new OpenApiContact
                    {
                        Name = "Desafio"
                    }
                });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Autenticação JWT usando o esquema Bearer. Exemplo: 'Bearer {token}'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                s.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                s.EnableAnnotations();
                s.UseAllOfToExtendReferenceSchemas();
                s.IgnoreObsoleteActions();
            });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Desafio";
                c.RoutePrefix = "swagger";
                c.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
                c.InjectJavascript("/SwaggerUI/js/swagger-ui.js");
                c.InjectStylesheet("/SwaggerUI/css/swagger-ui.css");
            });
        }
    }
}
