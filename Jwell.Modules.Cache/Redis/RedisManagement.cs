using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Cache.Redis
{
    public static class RedisManagement
    {
        public static ConnectionMultiplexer Connection { get; private set; }

        static RedisManagement()
        {
            ConfigurationOptions configurationOptions = new ConfigurationOptions
            {
                ClientName = "redisCache",
                Password = "qazwsxedc"
            };
            configurationOptions.EndPoints.Add("10.130.0.254", 6379);
            Connection = ConnectionMultiplexer.Connect(configurationOptions);
        }
    }
}
