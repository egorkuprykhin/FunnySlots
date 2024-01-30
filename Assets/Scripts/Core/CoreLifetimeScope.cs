using VContainer;
using VContainer.Unity;

namespace FunnySlots
{
    public class CoreLifetimeScope : LifetimeScope
    {
        public EcsStartup EcsStartup;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance<EcsStartup>(EcsStartup);
        }
    }
}