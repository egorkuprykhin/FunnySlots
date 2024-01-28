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
        
        public static void Set<TComponent>(this EcsWorld world, int entity) where TComponent : struct, IComponent
        {
            EcsPool<TComponent> ecsPool = world.GetPool<TComponent>();
            
            if (!ecsPool.Has(entity))
                ecsPool.Add(entity);
        }
        
        public static void SetEvent<TComponent>(this EcsWorld world, int entity) where TComponent : struct, IComponent
        {
            world.Get<TComponent>(entity);
        }
        
        public static void RemoveEvent<TComponent>(this EcsWorld world, int entity) where TComponent : struct, IComponent
        {
            world.GetPool<TComponent>().Del(entity);
        }
    }
}