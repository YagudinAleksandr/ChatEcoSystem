using ChatEcoSystem.Notification.Logic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChatEcoSystem.Notification.Worker
{
    public class Worker : BackgroundService
    {
        /// <inheritdoc cref="ILogger"/>
        private readonly ILogger<Worker> _logger;

        /// <inheritdoc cref="IServiceProvider"/>
        private readonly IServiceProvider _services;

        /// <summary>
        /// Интервал проверки
        /// </summary>
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(1);

        public Worker(ILogger<Worker> logger, IServiceProvider services)
        {
            _logger = logger;
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Notification Worker started");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _services.CreateScope())
                    {
                        var notificationService = scope.ServiceProvider
                            .GetRequiredService<INotificationService>();

                        await notificationService.ProcessNotificationsAsync();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in notification worker");
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }

            _logger.LogInformation("Notification Worker stopped");
        }
    }
}
