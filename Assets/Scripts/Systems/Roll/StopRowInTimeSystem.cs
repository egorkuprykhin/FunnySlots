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
                    cardData.IsMoving = false;
            }

            if (stopMovingEntity.Has<HighestCardInRow>(_world))
            {
                Debug.Log("WTF");
            }
            
            stopMovingEntity.Del<StopRowInTime>(_world);
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