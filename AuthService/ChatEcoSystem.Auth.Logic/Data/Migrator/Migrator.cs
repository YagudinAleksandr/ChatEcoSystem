using System;
using System.Threading.Tasks;
using ChatEcoSystem.SharedLib.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ChatEcoSystem.Auth.Logic.Data
{
    /// <inheritdoc cref="IMigrator"/>
    internal class Migrator : IMigrator
    {
        #region CTOR
        /// <inheritdoc cref="UserAuthContext"/>
        private readonly UserAuthContext _userAuthContext;

        /// <inheritdoc cref="ILogger"/>
        private readonly ILogger _logger;

        public Migrator(UserAuthContext userAuthContext, ILogger<Migrator> logger)
        {
            _userAuthContext = userAuthContext;
            _logger = logger;
        }

        #endregion

        public async Task Apply()
        {
            try
            {
                await _userAuthContext.Database.MigrateAsync();

                await SeedingDefaultData();
            }
            catch (Exception ex)
            {
                _logger.LogError("Can not seed database", ex);
            }
        }

        /// <summary>
        /// Сидирование базы
        /// </summary>
        private async Task SeedingDefaultData()
        {
            var dataSet = _userAuthContext.Set<User>();

            try
            {
                if (await dataSet.AnyAsync())
                {
                    return;
                }

                var defaultUser = new User()
                {
                    Name = "Test User",
                    Email = "test@mail.ru",
                    Password = "pa$$word",
                };

                await _userAuthContext.AddAsync(defaultUser);
                await _userAuthContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Can not create default user", ex);
            }
        }
    }
}
