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
                ref Vector2 position = ref entity.Get<CardPosition>(_world).Position;
                
                if (position.y < valueMaxOffsetToCreateCard)
                {
                    CreateCard(ref position, entity);
                }
            }
        }
        
        private void CreateCard(ref Vector2 position, int prevCardEntity)
        {
            bool isMoving = prevCardEntity.Get<CardMoving>(_world).IsMoving;
            Vector2 createdPosition = position + new Vector2(0, _configuration.Value.CellSize.y);

            int cardEntity = _world.Value.NewEntity();

            cardEntity.Get<CardMoving>(_world).IsMoving = isMoving;
            cardEntity.Get<CardPosition>(_world).Position = createdPosition;

            cardEntity.Set<CreateCardEvent>(_world);
            cardEntity.Set<SetCardSpriteEvent>(_world);
            cardEntity.Set<HighestCardInRow>(_world);
            
            prevCardEntity.Del<HighestCardInRow>(_world);
        }
    }
}