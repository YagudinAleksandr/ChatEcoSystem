using System;
using ChatEcoSystem.Chat.Logic.Data;
using ChatEcoSystem.Chat.Logic.Profiles;
using ChatEcoSystem.Chat.Logic.Repositories;
using ChatEcoSystem.Chat.Logic.Services;
using ChatEcoSystem.SharedLib.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace ChatEcoSystem.Chat.Logic
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавление через DI логики сервиса чатов
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <param name="configuration">Конфигурации</param>
        /// <returns>DI контейнер</returns>
        public static IServiceCollection AddChatServiceLogic(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ChatRoomProfile).Assembly);
            services.AddServices(configuration);
            services.AddRepositories();
            services.AddDbContext<ChatContext>(options =>
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
            return services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        }

        /// <summary>
        /// Добавление сервисов
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <returns>DI контейнер</returns>
        private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<JwtConfiguration>(configuration.GetSection("JwtConfiguration"));

            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
                options.KeepAliveInterval = TimeSpan.FromSeconds(15);
            });

            return services.AddScoped<IChatRoomService, ChatRoomService>()
                .AddScoped<IMessageService, MessageService>()
                .AddScoped<IChatRoomApiService, ChatRoomApiServiceAdapter>()
                .AddScoped<IMessageApiService, MessageApiServiceAdapter>()
                .AddSingleton<IPresenceTracker, PresenceTracker>();

        }
    }
}
