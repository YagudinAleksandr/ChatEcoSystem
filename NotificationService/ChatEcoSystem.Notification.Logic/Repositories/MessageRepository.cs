using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatEcoSystem.Notification.Logic
{
    /// <inheritdoc cref="IMessageRepository"/>
    internal class MessageRepository : IMessageRepository
    {
        #region CTOR
        /// <inheritdoc cref="NotificationDbContext"/>
        private readonly NotificationDbContext _context;

        /// <inheritdoc cref="ILogger"/>
        private readonly ILogger<MessageRepository> _logger;

        public MessageRepository(NotificationDbContext context, ILogger<MessageRepository> logger)
        {
            _logger = logger;
            _context = context;
        }
        #endregion

        /// <inheritdoc/>
        public async Task<List<UnreadMessage>> GetUnreadMessagesOlderThanAsync(TimeSpan minAge)
        {
            try
            {
                IQueryable<UnreadMessage> query = _context.Set<UnreadMessage>();

                var minTime = DateTime.UtcNow - minAge;

                return await query
                    .Where(m => m.SentTime < minTime)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving unread messages");
                throw;
            }
        }
    }
}
