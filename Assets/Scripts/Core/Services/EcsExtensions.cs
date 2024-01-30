using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots
{
    public static class EcsExtensions
    {
        public static bool Has<TComponent>(this int entity, EcsWorldInject world) where TComponent : struct, IComponent
        {
            return world.Value.GetPool<TComponent>().Has(entity);
        }
        
        public static ref TComponent Get<TComponent>(this int entity, EcsWorldInject world) where TComponent : struct, IComponent
        {
            return ref entity.Get<TComponent>(world.Value);
        }

        public static ref TComponent Get<TComponent>(this int entity, EcsWorld world) where TComponent : struct, IComponent
        {
            EcsPool<TComponent> ecsPool = world.GetPool<TComponent>();
            
            if (ecsPool.Has(entity))
                return ref ecsPool.Get(entity);
                    
            return ref ecsPool.Add(entity);
        }

        public static void Set<TComponent>(this int entity, EcsWorldInject world) where TComponent : struct, IComponent
        {
            entity.Set<TComponent>(world.Value);
        }

        public static void Set<TComponent>(this int entity, EcsWorld world) where TComponent : struct, IComponent
        {
            EcsPool<TComponent> ecsPool = world.GetPool<TComponent>();
            
            if (!ecsPool.Has(entity))
                ecsPool.Add(entity);
        }
        
        public static void Del<TComponent>(this int entity, EcsWorldInject world) where TComponent : struct, IComponent
        {
            entity.Del<TComponent>(world.Value);
        }
        
        public static void Del<TComponent>(this int entity, EcsWorld world) where TComponent : struct, IComponent
        {
            world.GetPool<TComponent>().Del(entity);
        }

        public static int NewEntity(this EcsWorldInject world)
        {
            return world.Value.NewEntity();
        }
    }
}