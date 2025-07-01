using ChatEcoSystem.Auth.Logic.Profiles;
using ChatEcoSystem.SharedLib.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ChatEcoSystem.Auth.Logic.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatEcoSystem.Auth.Logic
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавление через DI логики сервиса авторизации
        /// </summary>
        /// <param name="services">DI контейнер</param>
        /// <param name="configuration">Конфигурации</param>
        /// <returns>DI контейнер</returns>
        public static IServiceCollection AddAuthServiceLogic(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(UserDtoProfile).Assembly);
            services.AddServices(configuration);
            services.AddRepositories();
            services.AddDbContext<UserAuthContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

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
            services.Configure<JwtConfiguration>(configuration.GetSection("JwtConfiguration"));

            return services.AddScoped<IUserService, UserService>()
                .AddScoped<IAuthService, AuthService>();

        }
    }
}
