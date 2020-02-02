using System;
using System.Threading.Tasks;
using Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Web
{
    public static class DatabaseSeedInitialiser
    {
        public static IHost Seed(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            try
            {
                Task.Run(async () =>
                {
                    var dataseed = new DataInitialiser();
                    await dataseed.InitialiseData(serviceProvider);
                }).Wait();
            }
            catch (Exception e)
            {
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(e, "An error occurred seeding the DB.");
                throw;
            }

            return host;
        }
    }
}