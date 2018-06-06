using Jwell.Framework.Ioc;
using System;
using System.Threading.Tasks;

namespace Jwell.Modules.Cache
{
    [Component(ServiceLifetime.Transient)]
    public interface ICacheClient
    {
        /// <summary>
        /// 通过key获取缓存值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetCache<T>(string key);

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        Task<bool> SetCacheAsync<T>(string key, T value, int expireTime = 300);

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime"></param>
        bool SetCache<T>(string key, T value, int expireTime);

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime"></param>
        bool SetCache<T>(string key, T value, DateTime expireTime);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存key</param>
        void RemoveCache(string key);
    }
}
