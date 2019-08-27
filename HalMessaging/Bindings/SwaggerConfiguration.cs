using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace HalMessaging.Bindings
{
    public static class SwaggerConfiguration
    {

        public static void AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(ctx =>
            {
                ctx.DefaultApiVersion = new ApiVersion(1, 0);
                ctx.AssumeDefaultVersionWhenUnspecified = true;
                ctx.GroupNameFormat = "'v'VVV";
                ctx.SubstituteApiVersionInUrl = true;
            });

            services.AddApiVersioning(ctx =>
            {
                ctx.ReportApiVersions = true;
                ctx.AssumeDefaultVersionWhenUnspecified = true;
                ctx.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddSwaggerGen(options =>
            {
                var apiVersionDescriptionProvider
                = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, new OpenApiInfo
                    {
                        Version = description.ApiVersion.ToString(),
                        Title = $"HAL.Messaging.System {description.ApiVersion}",
                        Description = "Messagging System API",
                        TermsOfService = new Uri("https://www.huangal.com"),
                        Contact = new OpenApiContact
                        {
                            Name = "Henry Huangal",
                            Email = "halford.huangal@gmail.com",
                            Url = new Uri("https://www.huangal.com"),
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Use under DECH",
                            Url = new Uri("https://huangal.com/license"),
                        }
                    });
                }

                //options.SwaggerDoc("Admin", new OpenApiInfo
                //{
                //    Version = "0",
                //    Title = $"HAL.Messaging.System Admin",
                //    Description = "Messagging System API",
                //    TermsOfService = new Uri("https://www.huangal.com"),
                //    Contact = new OpenApiContact
                //    {
                //        Name = "Henry Huangal",
                //        Email = "halford.huangal@gmail.com",
                //        Url = new Uri("https://www.huangal.com"),
                //    },
                //    License = new OpenApiLicense
                //    {
                //        Name = "Use under DECH",
                //        Url = new Uri("https://huangal.com/license"),
                //    }
                //});


                // Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //options.IncludeXmlComments(xmlPath);


                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));


            });

        }


        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder builder, IApiVersionDescriptionProvider apiDescriptionProvider)
        {
            // builder.UseSwaggerAuthorized();
            //configure swagger
            builder.UseSwagger(options =>
            {
               options.RouteTemplate = "/api-docs/{documentName}/swagger.json";
              
            });

            //configure swaggerui
            builder.UseSwaggerUI(options =>
            {
                foreach (ApiVersionDescription desc in apiDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/api-docs/{desc.GroupName}/swagger.json", desc.GroupName.ToUpperInvariant());
                }

               // options.SwaggerEndpoint($"/api-docs/Admin/swagger.json", "Admin");


                options.RoutePrefix = string.Empty;

            });

            return builder;
        }


    }
}
