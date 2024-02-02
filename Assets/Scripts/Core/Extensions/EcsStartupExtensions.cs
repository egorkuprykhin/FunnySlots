using Leopotam.EcsLite;

namespace FunnySlots
{
    public static class EcsStartupExtensions
    {
        public static IEcsSystems Add<TSystem>(this IEcsSystems systems) where TSystem : IEcsSystem, new()
        {
            var system = new TSystem();
            systems.Add(system);
            
            return systems;
        }
    }
}