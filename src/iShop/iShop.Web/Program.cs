using System;
using System.IO;
using iShop.Repo;
using iShop.Repo.Helpers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Web;

namespace iShop.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Setting NLog, see nlog.config for the output configurations
            var logger = LogManager.LoadConfiguration(Path.GetFullPath("nlog.config")).GetCurrentClassLogger();
            try
            {
                logger.Info("Init Main Program.");
                var host = BuildWebHost(args);
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    try
                    {
                        // Set the databas
                        var context = services.GetRequiredService<ApplicationDbContext>();
                        var initializer = new AppInitializer(services, context);
                        initializer.Seed().Wait();
                    }
                    catch (Exception ex)
                    {
                        logger.Error($"An error occurred while seeding the database. The message is {ex.Message}");
                    }

                    host.Run();
                }
            }
            catch (Exception e)
            {
                logger.Error($"Stopped program because of {e.Message}");
                throw;
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseNLog()
                .Build();
    }
}
