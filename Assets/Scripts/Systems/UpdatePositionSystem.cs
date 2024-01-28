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
                entity.Get<CardViewRef>(_world.Value).CardView.transform.position =
                    entity.Get<CardPosition>(_world.Value).Position;
            }
        }
    }
}