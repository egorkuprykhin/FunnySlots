using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class CreateCardViewsSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<CardPosition, CreateCardEvent>, Exc<CardViewRef>> _filter;
        private EcsCustomInject<Configuration> _configuration;
        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter.Value)
            {
                Vector2 position = _world.Value.Get<CardPosition>(entity).Position;
                
                CardView cardPrefab = _configuration.Value.CardView;
                CardView instance = Object.Instantiate(cardPrefab, position, Quaternion.identity);

                _world.Value.Get<CardViewRef>(entity).Value = instance;
                _world.Value.RemoveEvent<CreateCardEvent>(entity);
            }
        }
    }
}