using ChatEcoSystem.SharedLib.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace ChatEcoSystem.Notification.Logic
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавление через DI логики сервиса оповещений
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <param name="configuration">Конфигурации</param>
        /// <returns>DI контейнер</returns>
        public static IServiceCollection AddNotificationServiceLogic(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServices(configuration);
            services.AddRepositories();
            services.AddDbContext<NotificationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IMigrator, Migrator>();
            services.AddSingleton<IConnectionMultiplexer>(provider =>
                ConnectionMultiplexer.Connect(configuration["Redis:Connection"]));

            return services;
        }

        /// <summary>
        /// Добавление репозиториев
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <returns>DI контейнер</returns>
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped<IMessageRepository, MessageRepository>();
        }

        /// <summary>
        /// Добавление сервисов
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <returns>DI контейнер</returns>
        private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SmtpConfiguration>(configuration.GetSection("SmtpConfiguration"));

            return services.AddScoped<INotificationService, NotificationService>()
                .AddScoped<IEmailService, EmailService>()
                .AddSingleton<IPresenceTracker, PresenceTracker>();

        }
    }
}
