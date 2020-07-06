using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using System;
using System.Threading.Tasks;

namespace Web.Common
{
    public class TicketStore : ITicketStore
    {
        private readonly string keyPrefix;
        private readonly IDistributedCache cache;

        public TicketStore(RedisCacheOptions options, string keyPrefix)
        {
            this.cache = new RedisCache(options);
            this.keyPrefix = keyPrefix;
        }

        public async Task<string> StoreAsync(AuthenticationTicket ticket)
        {
            var guid = Guid.NewGuid();
            var key = keyPrefix + guid.ToString();

            await RenewAsync(key, ticket);

            return key;
        }

        public async Task RenewAsync(string key, AuthenticationTicket ticket)
        {
            var options = new DistributedCacheEntryOptions();
            var expiresUtc = ticket.Properties.ExpiresUtc;

            if (expiresUtc.HasValue)
            {
                options.SetAbsoluteExpiration(expiresUtc.Value);
            }

            byte[] val = SerializeToBytes(ticket);

            await cache.SetAsync(key, val, options);
        }

        public async Task<AuthenticationTicket> RetrieveAsync(string key)
        {
            AuthenticationTicket ticket;
            byte[] bytes = null;

            bytes = await cache.GetAsync(key);
            ticket = DeserializeFromBytes(bytes);

            return ticket;
        }

        public async Task RemoveAsync(string key)
        {
            await cache.RemoveAsync(key);
        }

        private static byte[] SerializeToBytes(AuthenticationTicket source)
        {
            return TicketSerializer.Default.Serialize(source);
        }

        private static AuthenticationTicket DeserializeFromBytes(byte[] source)
        {
            return source == null ? null : TicketSerializer.Default.Deserialize(source);
        }
    }
}
