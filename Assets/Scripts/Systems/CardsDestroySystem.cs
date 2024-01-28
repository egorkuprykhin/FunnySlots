using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class CardsDestroySystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<DestroyCardEvent, CardViewRef>> _filter;
        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter.Value)
            {
                var cardView = entity.Get<CardViewRef>(_world.Value).CardView;
                
                Object.Destroy(cardView.gameObject);
                
                _world.Value.DelEntity(entity);
            }
        }
    }
}