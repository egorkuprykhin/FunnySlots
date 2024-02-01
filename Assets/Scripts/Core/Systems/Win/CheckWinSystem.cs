using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots
{
    public class CheckWinSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<StopRollEvent>> _stopRollEvent;
        private EcsFilterInject<Inc<CardData, CardViewRef>> _cards;
        
        private EcsFilterInject<Inc<CardWinFrameViewRef>> _winFrames;

        private EcsCustomInject<FieldPositionsService> _fieldPosService;
        private EcsCustomInject<Configuration> _configuration;
        
        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int stopRollEvent in _stopRollEvent.Value)
            {
                foreach (var card in _cards.Value)
                    if (CardInsideField(card))
                        AddCardInsideFieldComponent(card);

                DeleteCheckWinEvent(stopRollEvent);
            }
        }

        private bool CardInsideField(int card) => 
            _fieldPosService.Value.IsPositionInsideField(card.Get<CardData>(_world).Position);

        private void AddCardInsideFieldComponent(int card) => 
            card.Set<CardInsideField>(_world);

        private void DeleteCheckWinEvent(int checkWinEvent) => 
            checkWinEvent.Del<StopRollEvent>(_world);
    }
}