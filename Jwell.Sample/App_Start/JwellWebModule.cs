using Jwell.Application;
using Jwell.Framework.Modules;
using Jwell.Infrastructure;
using Jwell.Modules.WebApi;
using Jwell.Modules.MVC;
using Jwell.Modules.Cache;

namespace Jwell.Sample
{
    /// <summary>
    /// 模块加载
    /// </summary>
    [DependOn(typeof(MvcModule),typeof(WebApiModule),typeof(JwellApplicationModule),typeof(JwellRepositoryModule),
        typeof(JwellCacheModule))]
    public class JwellWebModule : JwellModule
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }
    }
}