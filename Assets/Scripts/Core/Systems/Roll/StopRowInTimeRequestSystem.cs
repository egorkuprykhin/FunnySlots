using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class StopRowInTimeRequestSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<StopRowInTime>> _movingCardRows;
        private EcsFilterInject<Inc<CardData>> _cards;

        private EcsCustomInject<Configuration> _configuration;
        private EcsCustomInject<FieldPositionsService> _fieldPositionsService;
        
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
            if (!cardEntity.Has<CardTargetPosition>())
                cardEntity.Get<CardTargetPosition>().Value =
                    GetTargetPositionForCard(cardEntity);
        }

        private Vector2 GetTargetPositionForCard(int cardEntity) => 
            _fieldPositionsService.Value.GetPositionOfNearBelowCell(cardEntity.Get<CardData>().Position);

        private void UpdateMovingRowTimings(int entity) => 
            entity.Get<StopRowInTime>().Timer += Time.deltaTime;

        private bool NeedStopRow(int entity) => 
            entity.Get<StopRowInTime>().Timer > entity.Get<StopRowInTime>().StopTime;

        private void ClearMovingRow(int movingCardsRowEntity) => 
            movingCardsRowEntity.Delete<StopRowInTime>();

        private bool CardsInSameRow(int cardEntity, int targetMovingRowEntity) => 
            cardEntity.Get<CardData>().Row == targetMovingRowEntity.Get<StopRowInTime>().Row;
    }
}