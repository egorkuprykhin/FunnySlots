using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class StopCardsMoveWatcherSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<StopMovingTimer, PassedTime>> _waitForMovingFilter;
        private EcsFilterInject<Inc<HighestCardMarker, CardPosition>> _highestMovingCards;
        private EcsFilterInject<Inc<CardPosition>> _movingCards;

        private EcsCustomInject<Configuration> _configuration;
        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            return;
            WaitTimerThenStopMoving();
        }

        private void WaitTimerThenStopMoving()
        {
            foreach (int waitForStopMovingEntity in _waitForMovingFilter.Value)
            {
                ref var passedTime = ref waitForStopMovingEntity.Get<PassedTime>(_world);
                ref var aliveTime = ref waitForStopMovingEntity.Get<StopMovingTimer>(_world);

                if (NeedStop(passedTime.Value, aliveTime.Value))
                    StopMoveColumn(waitForStopMovingEntity);

                passedTime.Value += Time.deltaTime;
            }
        }

        private void StopMoveColumn(int waitForStopMovingEntity)
        {
            var highestCardEntity = GetLeftHighestMovingCardEntity();
            if (highestCardEntity != -1)
            {
                highestCardEntity.Get<TargetPositionY>(_world).Value =
                    _configuration.Value.CellSize.y * (0.5f - 0.5f * _configuration.Value.FieldSize.y);
                
                var posX = highestCardEntity.Get<CardPosition>(_world).Value.x;
                
                PrepareToStopAllCardsInColumn(posX);
            }
            
            waitForStopMovingEntity.Del<StopMovingTimer>(_world);
            waitForStopMovingEntity.Del<PassedTime>(_world);
        }

        private void PrepareToStopAllCardsInColumn(float targetPosX)
        {
            var epsilon = _configuration.Value.Epsilon;
            
            foreach (int movingCardEntity in _movingCards.Value)
            {
                var cardPosX = movingCardEntity.Get<CardPosition>(_world).Value.x;

                // if (Mathf.Abs(cardPosX - targetPosX) <= epsilon)
                    // movingCardEntity.Get<CardData>(_world).WaitForStop = true;
            }
        }

        private bool NeedStop(float passedTime, float fullTime)
        {
            return passedTime >= fullTime;
        }

        private int GetLeftHighestMovingCardEntity()
        {
            float minPosX = float.MaxValue;
            int minPosXEntity = -1;
            
            foreach (int cardEntity in _highestMovingCards.Value)
            {
                ref var cardPos = ref cardEntity.Get<CardPosition>(_world);
                ref var cardMoving = ref cardEntity.Get<CardData>(_world);
                
                // bool cardIsMoving = cardMoving is { IsMoving: true, WaitForStop: false };

                // if (cardPos.Value.x < minPosX && cardIsMoving)
                {
                    minPosX = cardPos.Value.x;
                    minPosXEntity = cardEntity;
                }
            }

            return minPosXEntity;
        }
    }
}