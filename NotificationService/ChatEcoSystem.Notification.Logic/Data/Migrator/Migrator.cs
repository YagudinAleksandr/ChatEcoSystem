using ChatEcoSystem.SharedLib.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ChatEcoSystem.Notification.Logic
{
    /// <inheritdoc cref="IMigrator"/>
    internal class Migrator : IMigrator
    {
        #region CTOR
        /// <inheritdoc cref="NotificationDbContext"/>
        private readonly NotificationDbContext _context;

        /// <inheritdoc cref="ILogger"/>
        private readonly ILogger _logger;

        public Migrator(NotificationDbContext context, ILogger<Migrator> logger)
        {
            _context = context;
            _logger = logger;
        }

        #endregion
        public async Task Apply()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Can not seed database", ex);
            }
        }
    }
}
