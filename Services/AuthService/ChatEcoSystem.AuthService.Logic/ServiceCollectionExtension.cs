using ChatEcoSystem.Abstractions;
using ChatEcoSystem.AuthService.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatEcoSystem.AuthService.Logic
{
    /// <summary>
    /// DI для сервиса пользователей
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Подключение зависимостей сервиса авторизации
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <param name="configuration">Конфигурация</param>
        /// <returns>DI контейнер</returns>
        public static IServiceCollection AddAuthUserService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbUserContext(configuration);
            services.AddUserAuthRepository();
            services.AddUserAuthServices();

            return services;
        }

        /// <summary>
        /// Подключение сервисов базы данных
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <param name="configuration">Конфигурация</param>
        /// <returns>DI контейнер</returns>
        private static IServiceCollection AddDbUserContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserAuthDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AuthDb")));

            return services;
        }

        /// <summary>
        /// Добавление репозиториев
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <returns>DI контейнер</returns>
        private static IServiceCollection AddUserAuthRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

            return services;
        }

        /// <summary>
        /// Добавление сервисов
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <returns>DI контейнер</returns>
        private static IServiceCollection AddUserAuthServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
