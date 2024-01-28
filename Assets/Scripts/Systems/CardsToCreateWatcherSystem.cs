using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class CardsToCreateWatcherSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<HighestCardInRow, CardPosition, CardMoving, CardViewRef>> _filter;
        private EcsCustomInject<Configuration> _configuration;
        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter.Value)
            {
                float valueMaxOffsetToCreateCard = _configuration.Value.MaxOffsetToCreateCard * _configuration.Value.CellSize.y;
                ref Vector2 position = ref entity.Get<CardPosition>(_world.Value).Position;
                
                if (position.y < valueMaxOffsetToCreateCard)
                {
                    CreateCard(ref position, entity);
                }
            }
        }
        
        private void CreateCard(ref Vector2 position, int entity)
        {
            Vector2 createdPosition = position + new Vector2(0, _configuration.Value.CellSize.y);
            bool isMoving = entity.Get<CardMoving>(_world.Value).IsMoving;

            int cardEntity = _world.Value.NewEntity();
            
            cardEntity.Get<CardPosition>(_world.Value).Position = createdPosition;
            cardEntity.Get<CardMoving>(_world.Value).IsMoving = isMoving;
            cardEntity.Set<CreateCardEvent>(_world.Value);
            cardEntity.Set<HighestCardInRow>(_world.Value);
            
            entity.Del<HighestCardInRow>(_world.Value);
        }
    }
}