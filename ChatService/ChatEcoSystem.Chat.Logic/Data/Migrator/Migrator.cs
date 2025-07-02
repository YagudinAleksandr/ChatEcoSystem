using System;
using System.Threading.Tasks;
using ChatEcoSystem.Chat.Logic.Data;
using ChatEcoSystem.SharedLib.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ChatEcoSystem.Chat.Logic
{
    /// <inheritdoc cref="IMigrator"/>
    internal class Migrator : IMigrator
    {
        #region CTOR
        /// <inheritdoc cref="UserAuthContext"/>
        private readonly ChatContext _chatContext;

        /// <inheritdoc cref="ILogger"/>
        private readonly ILogger _logger;

        public Migrator(ChatContext chatContext, ILogger<Migrator> logger)
        {
            _chatContext = chatContext;
            _logger = logger;
        }

        #endregion

        public async Task Apply()
        {
            try
            {
                await _chatContext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Can not seed database", ex);
            }
        }
    }
}
