using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class StopRowInTimeSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<StopRowInTime>> _movingCardRows;
        private EcsFilterInject<Inc<CardData>> _cards;

        private EcsCustomInject<Configuration> _configuration;
        private EcsCustomInject<CardPositionsService> _fieldPositionsService;
        
        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (var movingCardsRowEntity in _movingCardRows.Value)
            {
                UpdateMovingRowTimings(movingCardsRowEntity);
                
                if (NeedStopRow(movingCardsRowEntity))
                    RequestStopRow(movingCardsRowEntity);
            }
        }

        private void RequestStopRow(int movingCardsRowEntity)
        {
            foreach (int cardEntity in _cards.Value)
                if (CardsInSameRow(cardEntity, movingCardsRowEntity)) 
                    ScheduleStopAtTargetPosition(cardEntity);

            ClearMovingRow(movingCardsRowEntity);
        }

        private void ScheduleStopAtTargetPosition(int cardEntity)
        {
            if (!cardEntity.Has<TargetPosition>(_world))
                cardEntity.Get<TargetPosition>(_world).Value =
                    GetTargetPositionForCard(cardEntity);
        }

        private Vector2 GetTargetPositionForCard(int cardEntity) => 
            _fieldPositionsService.Value.GetPositionOfNearBelowCell(cardEntity.Get<CardData>(_world).Position);

        private void UpdateMovingRowTimings(int entity) => 
            entity.Get<StopRowInTime>(_world).Timer += Time.deltaTime;

        private bool NeedStopRow(int entity) => 
            entity.Get<StopRowInTime>(_world).Timer > entity.Get<StopRowInTime>(_world).StopTime;

        private void ClearMovingRow(int movingCardsRowEntity) => 
            movingCardsRowEntity.Del<StopRowInTime>(_world);

        private bool CardsInSameRow(int cardEntity, int targetMovingRowEntity) => 
            cardEntity.Get<CardData>(_world).Row == targetMovingRowEntity.Get<StopRowInTime>(_world).Row;
    }
}