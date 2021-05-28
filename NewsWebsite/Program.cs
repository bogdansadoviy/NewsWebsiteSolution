using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NewsWebsite.DataAccess;
using NewsWebsite.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewsWebsite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            CreateDbIfNotExists(host);

            host.Run();
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    context.Database.EnsureCreated();
                    SetDefaultValue(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        private static void SetDefaultValue(ApplicationDbContext context)
        {
            if (!context.Tags.ToList().Any())
            {
                context.Tags.AddRange(new List<Tag>() 
                {
                    new Tag() { Id = 1, Name = "Суспільство" },
                    new Tag() { Id = 2, Name = "Політика" },
                    new Tag() { Id = 3, Name = "Економіка" },
                    new Tag() { Id = 4, Name = "За кордоном" },
                    new Tag() { Id = 5, Name = "Курйози" },
                });

                context.SaveChanges();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
