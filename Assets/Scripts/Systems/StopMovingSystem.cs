using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class StopMovingSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<CardPosition, TargetPositionY>> _targetPositionCardsFilter;
        private EcsFilterInject<Inc<CardMoving>> _movingCardsFilter;

        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            bool stopAllCards = false;

            foreach (var entity in _targetPositionCardsFilter.Value)
            {

                ref var targetPosition = ref entity.Get<TargetPositionY>(_world);
                var targetPositionY = targetPosition.Value;

                ref var cardPosition = ref entity.Get<CardPosition>(_world);
                var positionY = cardPosition.Value.y;

                if (positionY <= targetPositionY)
                {
                    cardPosition.Value = new Vector2(cardPosition.Value.x, targetPosition.Value);
                    entity.Del<TargetPositionY>(_world);

                    stopAllCards = true;
                }
            }

            if (stopAllCards)
                foreach (int movingCardEntity in _movingCardsFilter.Value)
                    movingCardEntity.Get<CardMoving>(_world).IsMoving = false;
        }
    }
    
}