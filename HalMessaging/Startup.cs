using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HalMessaging.Bindings;
using HalMessaging.Contracts;
using HalMessaging.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;             
using Microsoft.OpenApi.Models;

//[assembly: ApiConventionType(typeof(RestApiConventions))]

namespace HalMessaging
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddMiddlewareAnalysis();
            services.AddMvc(options =>
            {
                options.Filters.Add(new ConsumesAttribute("application/json"));
                options.Filters.Add(new ProducesAttribute("application/json"));
                



                //options.OutputFormatters.RemoveType<StringOutputFormatter>();
                //options.OutputFormatters.RemoveType<TextOutputFormatter>();
                //options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();

                options.ReturnHttpNotAcceptable = true;

                //var jsonOutputFormatter = options.OutputFormatters
                //   .OfType<JsonOutputFormatter>().FirstOrDefault();

                //if (jsonOutputFormatter != null)
                //{
                //    if (jsonOutputFormatter.SupportedMediaTypes.Contains("text/json"))
                //        jsonOutputFormatter.SupportedMediaTypes.Remove("text/json");

                //}



                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(StatusModel), StatusCodes.Status400BadRequest));
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(StatusModel), StatusCodes.Status406NotAcceptable));
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(StatusModel), StatusCodes.Status500InternalServerError));
                options.Filters.Add(new ProducesDefaultResponseTypeAttribute());
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(StatusModel), StatusCodes.Status401Unauthorized));




            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.RegisterServices(Configuration);

            services.AddSwaggerConfig();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwaggerConfig(apiVersionDescriptionProvider);

            app.UseMvc();
        }
    }
}
