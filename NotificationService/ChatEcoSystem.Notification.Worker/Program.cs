using ChatEcoSystem.Notification.Logic;
using ChatEcoSystem.SharedLib.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatEcoSystem.Notification.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Run migrations before starting the worker
            using (var scope = host.Services.CreateScope())
            {
                var migrator = scope.ServiceProvider.GetRequiredService<IMigrator>();
                migrator.Apply().GetAwaiter().GetResult();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddNotificationServiceLogic(hostContext.Configuration);
                    services.AddHostedService<Worker>();
                });
    }
}
