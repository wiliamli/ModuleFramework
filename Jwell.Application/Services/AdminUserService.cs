using Jwell.Application.Services;
using Jwell.Application.Services.Dtos;
using Jwell.Domain.Entities;
using Jwell.Framework.Paging;
using Jwell.Framework.Application.Service;
using Jwell.Framework.Extensions;
using Jwell.Modules.Cache;
using Jwell.Modules.MessageQueue.Redis;
using Jwell.Repository.Repositories;
using Jwell.Framework.Domain.Uow;

namespace Jwell.Application
{
    public class AdminUserService : ApplicationService, IAdminUserService
    {
        private IAdminUserRepository Repository { get; set; }
        private IHCacheClient CacheClient { get; set; }
        private IRedisMQ RedisMQ { get; set; }

        private IRedisListMQ RedisListMQ { get; set; }

        public AdminUserService(IAdminUserRepository repository, IHCacheClient cacheClient, 
            IRedisMQ redisMQ, IRedisListMQ redisListMQ)
        {
            Repository = repository;
            CacheClient = cacheClient;
            RedisMQ = redisMQ;
            RedisListMQ = redisListMQ;
        }

        [UnitOfWork(UseTransaction = true)]
        public int Count()
        {
            AdminUser adminUser = new AdminUser()
            {
                Account = "1234",
                Code = "12345"
            };

            AdminUser adminUser2 = adminUser.Clone<AdminUser>();
            adminUser2.Account = "adminUser2";

            //RedisMQ.Subscribe("adminUser",(chandle,message)=> {

            //    //先订阅
            //});

            //第一个元素定位
            RedisListMQ.RPush("a","1");
            RedisListMQ.LPush("a","2");
            //此时第一个元素应该是0
            RedisListMQ.RPush("a", "0");
            //此时最后一个元素应该是3
            RedisListMQ.LPush("a", "3");
            //总数量应该是4
            long length=RedisListMQ.ListLength("a");

            // 0
            string value = RedisListMQ.RPOP("a");
            // 3
            value = RedisListMQ.LPOP("a");
            // leng 2
            length = RedisListMQ.ListLength("a");

            //RedisMQ.Publish<AdminUser>("adminUser", adminUser);

            //System.Collections.Concurrent.ConcurrentDictionary<string, string> dic = 
            //    new System.Collections.Concurrent.ConcurrentDictionary<string, string>();
            //System.Collections.Generic.KeyValuePair<string, string> keyValue = 
            //    new System.Collections.Generic.KeyValuePair<string, string>("a","b");

            //dic.AddIfNotContains(keyValue);
            //CacheClient.DB = 2;
            //CacheClient.SetHT("test", dic);
            //CacheClient.SetHV("test", "123", "456");
            //CacheClient.SetHV("test", "789", "abc");
            //bool success = CacheClient.IsExistH("test");

            //success = CacheClient.IsExistHV("test","a");
            //string a =CacheClient.GetHV("test","a");
            //var list =CacheClient.GetHValues("test");
            //success=CacheClient.RemoveHK("test","a");
            //success = CacheClient.ReomveHKS("test",new string[] { "123", "789"});

            //success = CacheClient.IsExist("test");

            //success = CacheClient.RemoveCache("test");
            return 0;//Repository.Queryable().Count();
        }

        public PageResult<AdminUserDto> GetAdminUserDtos(PageParam page)
        {
           return Repository.Queryable().ToPageResult(page).ToDtos();
        }
    }
}
