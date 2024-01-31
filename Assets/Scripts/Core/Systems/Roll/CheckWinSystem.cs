using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots
{
    public class CheckWinSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<CheckWinEvent>> _checkWinEvent;
        private EcsFilterInject<Inc<CardData, CardViewRef>> _cards;
        
        private EcsFilterInject<Inc<WinFrameViewRef>> _winFrames;

        private EcsCustomInject<CardPositionsService> _fieldPosService;
        private EcsCustomInject<Configuration> _configuration;
        
        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int checkWinEvent in _checkWinEvent.Value)
            {
                foreach (var card in _cards.Value)
                {
                    var pos = card.Get<CardData>(_world).Position;
                    
                    if (_fieldPosService.Value.IsPositionInsideField(pos))
                    {
                        card.Set<CardInsideField>(_world);
                    }
                }

                checkWinEvent.Del<CheckWinEvent>(_world);
                
                ResetRollingStateAfterWin(systems.GetShared<SharedData>());
            }
        }

        private async void ResetRollingStateAfterWin(SharedData sharedData)
        {
            await UniTask.Delay(_configuration.Value.WinTimeMs);
            
            sharedData.ResetRollingState();
        }
    }
}