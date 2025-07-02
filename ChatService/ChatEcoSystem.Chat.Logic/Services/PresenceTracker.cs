using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ChatEcoSystem.SharedLib.Contracts;
using StackExchange.Redis;

namespace ChatEcoSystem.Chat.Logic
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

        public async Task UserConnected(string userId, string connectionId)
        {
            await _redisDb.StringSetAsync($"status:{userId}", "online");
            await _redisDb.SetAddAsync("online_users", userId);
        }

        public async Task UserDisconnected(string userId, string connectionId)
        {
            await _redisDb.StringSetAsync($"status:{userId}", "offline");
            await _redisDb.SetRemoveAsync("online_users", userId);
        }

        public async Task<BaseApiResponse<bool>> IsUserOnline(string userId)
        {
            var status = await _redisDb.StringGetAsync($"status:{userId}");
            return new BaseApiResponse<bool>() { Body = status == "online", StatusCode = 200 };
        }
    }
}
