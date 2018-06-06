using Autofac;
using Jwell.Framework.Modules;
using Jwell.Infrastructure;
using Jwell.Modules.Cache;

namespace Jwell.Application
{
    [DependOn(typeof(JwellRepositoryModule),typeof(JwellCacheModule))]
    public class JwellApplicationModule: JwellModule
    {
        public override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }

        //public override void Loaded(IContainer container)
        //{
        //    base.Loaded(container);

        //    DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        //}
    }
}
