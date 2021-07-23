using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using ApiRest.Data.Converter.Implementations;
using ApiRest.Model.Context;
using ApiRest.Repository;
using ApiRest.Repository.Generic;
using ApiRest.Service;
using ApiRest.Service.Implementations;
using System;
using System.Collections.Generic;

namespace ApiRest
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            var connection = Configuration["MySQLConnection:MySQLConnectionString"];
            services.AddDbContext<MySQLContext>(
                options => options.UseMySql(connection, ServerVersion.AutoDetect(connection)));
            /*
             * new MySqlServerVersion(new Version(8, 0, 25) - comando pode ser usado caso autodetect não funcione
             */

            if (Environment.IsDevelopment())
            {
                MigrateDatabase(connection);
            }

            //Versioning API
            services.AddApiVersioning();

            /*
             * Dependencias injetadas pelo desenvolvedor
             */
            //1 dependencia para o modelo Person
            services.AddScoped<IPersonService, PersonServiceImplementation>();
            //2 dependencia para o modelo Book
            services.AddScoped<IBookService, BookServiceImplementation>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<PersonConverter>();

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /*
         * https://evolve-db.netlify.app/configuration/naming/
         */
        public void MigrateDatabase(string connection)
        {
            try
            {
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = true,
                };
                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed", ex);
                throw;
            }
        }
    }
}
