using Leopotam.EcsLite;

namespace FunnySlots
{
    public static class EcsExtensions
    {
        public static ref TComponent Get<TComponent>(this EcsWorld world, int entity) where TComponent : struct, IComponent
        {
            EcsPool<TComponent> ecsPool = world.GetPool<TComponent>();
            
            if (ecsPool.Has(entity))
                return ref ecsPool.Get(entity);
                    
            return ref ecsPool.Add(entity);
        }
    }
}