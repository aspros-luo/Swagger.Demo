using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Reflection;

namespace Swagger.Demo
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
            services.AddMvc();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Swagger API",
                    Description = "qwerty",
                });
                options.DocumentFilter<CustomDocumentFilter>();
                options.OperationFilter<CustomOperationFilter>();
                options.SchemaFilter<CustomSchemaFilter>();
                options.IncludeXmlComments(GetXmlCommentsPath(GetType().GetTypeInfo().Assembly.GetName().Name));
            });
        }
        private static string GetXmlCommentsPath(string name)
        {
            //获取项目dll 所在的bin目录
            return Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, name + ".xml");
            //            return string.Format(@"{0}\{1}.XML", PlatformServices.Default.Application.ApplicationBasePath, name);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
