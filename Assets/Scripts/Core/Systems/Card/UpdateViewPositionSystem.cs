using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots
{
    public class UpdateViewPositionSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<CardData, CardViewRef>> _cards;
        
        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            if (_world.Value.IsAlive())
                foreach (int entity in _cards.Value)
                    entity.Get<CardViewRef>(_world).CardView.transform.position =
                        entity.Get<CardData>(_world).Position;
        }
    }
}