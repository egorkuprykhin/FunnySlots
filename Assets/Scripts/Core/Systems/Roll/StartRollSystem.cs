using Cysharp.Threading.Tasks;
using FunnySlots.Sound;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FunnySlots
{
    public class StartRollSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<StartRollEvent>> _startRollEvent;
        private EcsFilterInject<Inc<HighestCardInRow>> _highestCards;
        
        private EcsFilterInject<Inc<CardInsideField>> _cardsToClean;
        
        private EcsFilterInject<Inc<CardData, CardViewRef>> _cards;

        private EcsCustomInject<Configuration> _configuration;
        private EcsCustomInject<SceneData> _sceneData;

        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int startRollEvent in _startRollEvent.Value)
            {
                ClearLastCards();
                CreateStopRowTimingsAndEntities();
                StartRollCards();
                PlayRollSound();
                
                startRollEvent.Del<StartRollEvent>(_world);
            }
        }

        private void PlayRollSound()
        {
            ref var playSoundEvent = ref _world.NewEntity().Get<PlaySoundEvent>(_world);
            playSoundEvent.Type = CoreSound.RollStarted;
            playSoundEvent.NeedPlay = true;
        }

        private void ClearLastCards()
        {
            foreach (int cardEntity in _cardsToClean.Value)
                if (cardEntity.Has<WinFrameViewRef>(_world))
                {
                    Object.Destroy(cardEntity.Get<WinFrameViewRef>(_world).Value.gameObject);
                    cardEntity.Del<WinFrameViewRef>(_world);
                    cardEntity.Del<CardInsideField>(_world);
                }
        }

        private async void CreateStopRowTimingsAndEntities()
        {
            float rollingTimeSeed = CalculateRollingTime();
            float stoppingTimeSeed = CalculateStoppingTime();
            
            foreach (int highestCardEntity in _highestCards.Value)
            {
                int row = highestCardEntity.Get<HighestCardInRow>(_world).Row;
                var stoppingTime = CalculateStopTimingForRow(row, rollingTimeSeed, stoppingTimeSeed);
                
                CreateStopRowInTimeEntity(row, stoppingTime);
                
            }

            float delay = rollingTimeSeed + stoppingTimeSeed * _configuration.Value.FieldSize.x;
            int delayMs = Mathf.CeilToInt(delay * 1000f);
            
            await UniTask.Delay(delayMs);
            await WaitUntilAllCardsStopped();

            if (_world.Value.IsAlive())
            {
                StopRollSound();
                _world.NewEntity().Set<CheckWinEvent>(_world);
            }
        }

        private void StopRollSound()
        {
            ref var playSoundEvent = ref _world.NewEntity().Get<PlaySoundEvent>(_world);
            playSoundEvent.Type = CoreSound.RollStopped;
            playSoundEvent.NeedPlay = false;
        }

        private async UniTask WaitUntilAllCardsStopped() =>
            await UniTask.WaitUntil(() =>
            {
                if (_world.Value.IsAlive())
                    foreach (int card in _cards.Value)
                        if (card.Get<CardData>(_world).IsMoving)
                            return false;
                return true;
            });

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
            var stopRowTiming = rollingTime + stoppingTime * row;
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