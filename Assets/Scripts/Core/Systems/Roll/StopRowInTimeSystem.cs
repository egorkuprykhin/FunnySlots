using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class StopRowInTimeSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<StopRowInTime>> _stopRowsInTime;
        private EcsFilterInject<Inc<CardData>> _cards;

        private EcsCustomInject<Configuration> _configuration;
        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (var stopMovingEntity in _stopRowsInTime.Value)
            {
                UpdateTimings(stopMovingEntity);
                
                if (NeedStopMoving(stopMovingEntity))
                    StopMoveRow(stopMovingEntity);
            }
        }

        private void StopMoveRow(int stopMovingEntity)
        {
            int stoppingRow = stopMovingEntity.Get<StopRowInTime>(_world).Row;

            foreach (int cardEntity in _cards.Value)
            {
                ref var cardData = ref cardEntity.Get<CardData>(_world);
                
                if (cardData.Row == stoppingRow)
                {
                    if (!cardEntity.Has<TargetPosition>(_world))
                        cardEntity.Get<TargetPosition>(_world).Value =
                            GetTargetPositionForCard(cardEntity);
                }
            }
            
            stopMovingEntity.Del<StopRowInTime>(_world);
        }

        private Vector2 GetTargetPositionForCard(int cardEntity)
        {
            var currentPosition = cardEntity.Get<CardData>(_world).Position;
            var targetPosition = GetNearCellPositionBelow(currentPosition);
            
            return targetPosition;
        }

        private Vector2 GetNearCellPositionBelow(Vector2 pos)
        {
            var cellSize = _configuration.Value.CellSize;
            var fieldSize = _configuration.Value.FieldSize;
            var minOffset = _configuration.Value.CellsOffsetToDestroyCard;

            float minTargetY = Mathf.Sign(-1) * cellSize.y * (1 + fieldSize.y);
            float targetY = minTargetY;
            
            while (targetY < pos.y)
                targetY += cellSize.y;
            targetY -= cellSize.y;

            return new Vector2(pos.x, targetY);
        }

        private void UpdateTimings(int entity)
        {
            entity.Get<StopRowInTime>(_world).Timer += Time.deltaTime;
        }

        private bool NeedStopMoving(int entity)
        {
            ref var stopRowInTime = ref entity.Get<StopRowInTime>(_world);
            return stopRowInTime.Timer > stopRowInTime.StopTime;
        }
    }
}