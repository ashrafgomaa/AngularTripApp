using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dreamlines.Backend.Data.JsonData;
using DreamLines.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Dreamlines.Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //Name of the Cors policy to be set and applied later on.
        string AllowSpecificOriginsPolicy = "";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //read the policy name from configurations
            AllowSpecificOriginsPolicy = Configuration.GetSection("CustomSettings").GetValue(typeof(string), "PolicyName").ToString();
            //Define and register the CORS for specific origins which stored in configurations
            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOriginsPolicy,
                builder =>
                {
                    builder.WithOrigins(Configuration.GetSection("CustomSettings").GetValue(typeof(string), "AllowedOrigins").ToString().Split(new char[] { ','})).AllowAnyMethod();
                });
            });

            //Configure the database context to initialize the entity framework using connection string stored in configurations
            services.AddDbContext<AppDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DreamLinesDBConnection")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else { app.UseHsts(); }

            //Allow CORS to the specific ploicy
            app.UseCors(AllowSpecificOriginsPolicy);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMvc();

            //Seeds data which sotred in json file inside the data folder to database
            DataSeeder.Initialize(app);
        }
    }
}
