using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots
{
    public class UpdatePositionSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<CardData, CardViewRef>> _filter;
        
        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter.Value)
            {
                entity.Get<CardViewRef>(_world).CardView.transform.position =
                    entity.Get<CardData>(_world).Position;
            }
        }
    }
}