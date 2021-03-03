using KIP_server_GET.Constants;
using KIP_server_GET.DB;
using KIP_server_GET.Interfaces;
using KIP_server_GET.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace KIP_server_GET
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Console.OutputEncoding = System.Text.Encoding.Default;
            


            /*
            var connection = Configuration.GetConnectionString("PostgresConnection");
            services.AddDbContext<Server_GETContext>(options => options.UseNpgsql(connection));
            services.AddTransient<IFaculty, FacultyDB>();
            */
            

            var pgConnectionString = this.Configuration.GetConnectionString("PostgresConnection");
            var pgVersionString = this.Configuration.GetConnectionString("PostgresVersion");

            services.AddDbServices(pgConnectionString, pgVersionString);






            services.AddMvcCore()
                    .AddDataAnnotations()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        public void Configure(IApplicationBuilder app, ILogger<Startup> logger, IWebHostEnvironment env)
        {
            app.UseTokens(this.Configuration["Tokens:EntryToken"]);
            var message = $"{CustomNames.KIP_server_GET} uses Tokens Protection";
            logger.Log(LogLevel.Information, message);


            /*
            using (ServerContext db = new ServerContext())
            {
                var users = db.Faculty.ToList();
                Console.WriteLine("Список объектов:");
                foreach (var u in users)
                {
                    Console.WriteLine($"{u.FacultyID}.{u.FacultyName}");
                }
            }

            */
            /*
            Server_GETContext cont;
            using (var scope = app.ApplicationServices.CreateScope())
            {
                cont = scope.ServiceProvider.GetRequiredService<Server_GETContext>();
                cont.SaveChanges();
            }
            */
            //if (!cont.Faculty.Any())
            //cont.Faculty.AddRange(Faculties.Select(c => c.Value));







            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseEndpoints(builder =>
            {
                builder.MapControllers();
            });

            app.UseResponseCaching();
        }







        /*
        private static Dictionary<string, Faculty> faculties;
        public static Dictionary<string, Faculty> Faculties
        {
            get
            {
                if (faculties == null)
                {
                    var list = new Faculty[]
                    {
                        new Faculty(21, "КН", "Комп'ютерних наук та програмної інженерії"),
                        new Faculty(42, "КІТ", "Комп'ютерних та інформаційних технологій")
                    };

                    faculties = new Dictionary<string, Faculty>();
                    foreach(var l in list)
                    {
                        faculties.Add(l.FacultyName, l);
                    }
                }

                return faculties;
            }
        }
        */
    }
}
