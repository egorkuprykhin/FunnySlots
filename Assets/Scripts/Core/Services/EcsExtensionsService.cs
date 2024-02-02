using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots
{
    public static class EcsExtensionsService
    {
        public static EcsWorld World { get; set; }

        public static int NewEntity(this EcsWorldInject world) => 
            world.Value.NewEntity();
        
        public static ref TComponent Create<TComponent>(this EcsWorldInject world) where TComponent : struct, IComponent => 
            ref world.Value.NewEntity().Get<TComponent>();

        public static void Delete<TComponent>(this EcsWorldInject world, int entity) where TComponent : struct, IComponent => 
            world.Value.GetPool<TComponent>().Del(entity);

        public static bool Has<TComponent>(this int entity) where TComponent : struct, IComponent => 
            World.GetPool<TComponent>().Has(entity);

        public static ref TComponent Get<TComponent>(this int entity) where TComponent : struct, IComponent
        {
            EcsPool<TComponent> ecsPool = World.GetPool<TComponent>();
            
            if (ecsPool.Has(entity))
                return ref ecsPool.Get(entity);
                    
            return ref ecsPool.Add(entity);
        }

        public static void Set<TComponent>(this int entity) where TComponent : struct, IComponent
        {
            EcsPool<TComponent> ecsPool = World.GetPool<TComponent>();
            
            if (!ecsPool.Has(entity))
                ecsPool.Add(entity);
        }

        public static void Del<TComponent>(this int entity) where TComponent : struct, IComponent => 
            World.GetPool<TComponent>().Del(entity);
    }
}