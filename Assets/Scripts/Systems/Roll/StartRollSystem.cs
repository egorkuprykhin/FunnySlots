using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class StartRollSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<StartRollEvent>> _startRollEvent;
        private EcsFilterInject<Inc<HighestCardInRow>> _highestCards;
        
        private EcsFilterInject<Inc<CardData, CardViewRef>> _cards;

        private EcsCustomInject<Configuration> _configuration;

        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int startRollEvent in _startRollEvent.Value)
            {
                CreateStopRowTimingsAndEntities();
                
                StartRollCards();
                
                startRollEvent.Del<StartRollEvent>(_world);
            }
        }

        private void CreateStopRowTimingsAndEntities()
        {
            float rollingTime = CalculateRollingTime();
            float stoppingTime = CalculateStoppingTime();
            
            foreach (int highestCardEntity in _highestCards.Value)
            {
                int row = highestCardEntity.Get<HighestCardInRow>(_world).Row;
                float rowStoppingTime = CalculateStopTimingForRow(row, rollingTime, stoppingTime);

                CreateStopRowInTimeEntity(row, rowStoppingTime);
            }
        }

        private void StartRollCards()
        {
            foreach (int card in _cards.Value)
                card.Get<CardData>(_world).IsMoving = true;
        }

        private void CreateStopRowInTimeEntity(int row, float stopTime)
        {
            int rowEntity = _world.NewEntity();
            
            rowEntity.Get<StopRowInTime>(_world).Row = row;
            rowEntity.Get<StopRowInTime>(_world).StopTime = stopTime;
            rowEntity.Get<StopRowInTime>(_world).Timer = 0;
        }

        private float CalculateStopTimingForRow(int row, float rollingTime, float stoppingTime)
        {
            var stopRowTiming = rollingTime + row * stoppingTime;
            return stopRowTiming;
        }

        private float CalculateRollingTime()
        {
            return Random.Range(
                _configuration.Value.RollingTimeRange.x,
                _configuration.Value.RollingTimeRange.y);
        }

        private float CalculateStoppingTime()
        {
            return Random.Range(
                _configuration.Value.StoppingTimeRange.x,
                _configuration.Value.StoppingTimeRange.y);
        }
    }
}