using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class StopRollingWatcherSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<WaitForStopMoving>> _waitForMovingFilter;
        private EcsFilterInject<Inc<HighestCardMarker>> _highestCardsFilter;

        private EcsCustomInject<Configuration> _configuration;
        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int waitForStopMovingEntity in _waitForMovingFilter.Value)
            {
                ref PassedTime passedTime = ref waitForStopMovingEntity.Get<PassedTime>(_world);
                passedTime.Time += Time.deltaTime;

                float rollingTime = waitForStopMovingEntity.Get<WaitForStopMoving>(_world).RollingTime;
                
                if (NeedStop(passedTime.Time, rollingTime))
                {
                    foreach (int highestCardEntity in _highestCardsFilter.Value)
                    {
                        highestCardEntity
                            .Get<TargetPositionY>(_world)
                            .Value = _configuration.Value.CellSize.y * (0.5f - 0.5f * _configuration.Value.FieldSize.y);
                    }
                    
                    waitForStopMovingEntity.Del<WaitForStopMoving>(_world);
                    waitForStopMovingEntity.Del<PassedTime>(_world);
                }
            }
        }

        private bool NeedStop(float passedTime, float fullTime)
        {
            return passedTime >= fullTime;
        }
    }
}