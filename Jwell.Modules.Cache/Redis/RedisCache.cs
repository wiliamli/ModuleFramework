using StackExchange.Redis;
using System;
using Jwell.Framework.Utilities;
using System.Threading.Tasks;

namespace Jwell.Modules.Cache.Redis
{
    public sealed class RedisCache
    {
        /// <summary>
        /// redis配置文件信息
        /// </summary>
        private static ConnectionMultiplexer connection;
        private static IDatabase database;
        private const long TICKCARDINAL = 10000000;

        static RedisCache()
        {
            connection = RedisManagement.Connection;
            database = connection.GetDatabase(0);
        }

        public T GetString<T>(string key)
        {
            T t = default(T);
            RedisValue redisValue = database.StringGet(key);
            if (!redisValue.IsNullOrEmpty)
            {
                t =  Serializer.FromJson<T>(redisValue.ToString());
            }
            return t;
        }
         
        public void RemoveCache(string key)
        {
            database.KeyDelete(key);
        }

        public async Task<bool> SetCacheAsync<T>(string key, T value, long expireTime)
        {
            bool result = await database.StringSetAsync(key, Serializer.ToJson(value),
                new TimeSpan(expireTime * TICKCARDINAL)); //timespan以100毫秒为单位
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime">秒为单位</param>
        /// <returns></returns>
        public bool SetCache<T>(string key, T value, long expireTime)
        {
            bool result = database.StringSet(key, Serializer.ToJson(value), 
                new TimeSpan(expireTime * TICKCARDINAL)); //timespan以100毫秒为单位
            return result;
        }
    }
}
