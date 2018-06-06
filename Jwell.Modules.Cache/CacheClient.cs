using System;
using System.Threading.Tasks;

namespace Jwell.Modules.Cache
{
    public class CacheClient : ICacheClient
    {
        private Redis.RedisCache redis = new Redis.RedisCache();

        public T GetCache<T>(string key)
        {
           return redis.GetString<T>(key);
        }

        public void RemoveCache(string key)
        {
            redis.RemoveCache(key);
        }

        public async Task<bool> SetCacheAsync<T>(string key, T value, int expireTime = 300)
        {
            bool result = await redis.SetCacheAsync(key, value, expireTime);
            return result;
        }

        public bool SetCache<T>(string key, T value, int expireTime)
        {
            bool result = redis.SetCache(key, value, expireTime);
            return result;
        }

        public bool SetCache<T>(string key, T value, DateTime expireTime)
        {
            long timeSpan = (expireTime - DateTime.Now).Ticks;
            bool result = redis.SetCache(key, value, timeSpan);
            return result;
        }
    }
}
