using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots
{
    public class UpdatePositionSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<CardPosition, CardViewRef>> _filter;
        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter.Value)
            {
                _world.Value.Get<CardViewRef>(entity).Value.transform.position =
                    _world.Value.Get<CardPosition>(entity).Position;
            }
        }
    }
}