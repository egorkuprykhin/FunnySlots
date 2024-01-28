using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class CardsCreateSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<CreateCardEvent, CardPosition>, Exc<CardViewRef>> _filter;
        private EcsCustomInject<Configuration> _configuration;
        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter.Value)
            {
                Vector2 position = entity.Get<CardPosition>(_world).Value;
                
                CardView cardPrefab = _configuration.Value.CardView;
                CardView instance = Object.Instantiate(cardPrefab, position, Quaternion.identity);

                entity.Get<CardViewRef>(_world).CardView = instance;
                
                entity.Del<CreateCardEvent>(_world);
            }
        }
    }
}