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
            foreach (int cardEntity in _cards.Value) 
                SetViewPosition(cardEntity);
        }

        private void SetViewPosition(int entity) =>
            entity.Get<CardViewRef>().CardView.transform.position =
                entity.Get<CardData>().Position;
    }
}