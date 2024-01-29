using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class DestroyOldCardsSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<CardData, CardViewRef>, Exc<HighestCardInRow, DestroyCardEvent>> _cards;
        
        private EcsCustomInject<Configuration> _configuration;
        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            var destroyCardOffset = 
                Mathf.Sign(-1) * (_configuration.Value.CellsOffsetToDestroyCard * _configuration.Value.CellSize.y);
            
            foreach (int entity in _cards.Value)
            {
                Vector2 position = entity.Get<CardData>(_world).Position;
                
                if (position.y < destroyCardOffset) 
                    entity.Set<DestroyCardEvent>(_world);
            }
        }
    }
}