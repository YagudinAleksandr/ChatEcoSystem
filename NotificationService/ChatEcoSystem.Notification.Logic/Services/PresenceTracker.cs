using StackExchange.Redis;
using System.Threading.Tasks;

namespace ChatEcoSystem.Notification.Logic
{
    /// <inheritdoc cref="IPresenceTracker"/>
    internal class PresenceTracker : IPresenceTracker
    {
        #region CTOR
        /// <inheritdoc cref="IDatabase"/>
        private readonly IDatabase _redisDb;

        public PresenceTracker(IConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }
        #endregion

        public async Task<bool> IsUserOnline(string userId)
        {
            return await _redisDb.SetContainsAsync("online_users", userId);
        }
    }
}
