using LinenAndBird.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinenAndBird;

namespace LinenAndBird
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            // from appsettinsg.json
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // registering a service = telling asp.net how to build a thing.
            // services.AddTransient<IConfiguration>() -> create a new thing anytime someone asks for 
            // a configuration
            // services.AddScoped<IConfiguration>() -> create a new thing once per http request
            services.AddSingleton<IConfiguration>(Configuration); /* -> any time someone asks for this thing,
                                                                        give them the same copy */
            // we will register every repository as a transient
            services.AddTransient<BirdRepository>(); // create a new thing anytime someone asks
            services.AddTransient<OrdersRepository>(); // create a new thing anytime someone asks
            services.AddTransient<HatRepository>(); // create a new thing anytime someone asks
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LinenAndBird", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LinenAndBird v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
