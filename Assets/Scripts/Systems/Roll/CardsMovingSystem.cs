using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class CardsMovingSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<StartCardsMoveEvent>> _startMoveEvent;
        private EcsFilterInject<Inc<StopCardsMoveEvent>> _stopMoveEvent;
        private EcsFilterInject<Inc<CardData>> _cards;

        private EcsCustomInject<Configuration> _configuration;
        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            // float rollingTime = Random.Range(
            //     _configuration.Value.RollingTimeRange.x,
            //     _configuration.Value.RollingTimeRange.y);

            foreach (int eventEntity in _startMoveEvent.Value)
            {
                foreach (int card in _cards.Value) 
                    card.Get<CardData>(_world).IsMoving = true;
                
                eventEntity.Del<StartCardsMoveEvent>(_world);
            }

            foreach (int eventEntity in _stopMoveEvent.Value)
            {
                foreach (int card in _cards.Value) 
                    card.Get<CardData>(_world).IsMoving = false;
                
                eventEntity.Del<StopCardsMoveEvent>(_world);
            }


            // foreach (int entity in _startMoveEventFilter.Value)
            // {
            //     int _columnsCount = _configuration.Value.FieldSize.x;
            //
            //     for (int i = 0; i < _columnsCount; i++)
            //     {
            //         float stoppingTime =
            //             Random.Range(
            //                 _configuration.Value.StoppingTimeRange.x,
            //                 _configuration.Value.StoppingTimeRange.y)
            //             * (i + 1);
            //         
            //         CreateCardGroupStopMoveTimer(rollingTime + stoppingTime);
            //     }
            //     
            //     entity.Del<StartCardsMoveEvent>(_world);
            // }
        }

        private void CreateCardGroupStopMoveTimer(float fullRollingTime)
        {
            int startMoveCardsGroupEntity = _world.NewEntity();
            
            startMoveCardsGroupEntity.Get<StopMovingTimer>(_world).Value = fullRollingTime;
            startMoveCardsGroupEntity.Get<PassedTime>(_world).Value = 0;
        }
    }
}