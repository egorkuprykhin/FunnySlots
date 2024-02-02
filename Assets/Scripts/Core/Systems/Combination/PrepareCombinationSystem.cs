using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots
{
    public class PrepareCombinationSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<StopRollEvent>> _stopRollEvent;
        private EcsFilterInject<Inc<CardData, CardViewRef>> _cards;
        
        private EcsCustomInject<FieldPositionsService> _fieldPositionsService;
        
        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int stopRollEventEntity in _stopRollEvent.Value)
            {
                SetCombinationCards();
                
                _world.Create<SelectWinCombinationEvent>();
                
                stopRollEventEntity.Delete<StopRollEvent>();
            }
        }

        private void SetCombinationCards()
        {
            foreach (var card in _cards.Value)
                if (CardInsideField(card))
                    SetCombinationCard(card);
        }

        private bool CardInsideField(int cardEntity) => 
            _fieldPositionsService.Value.IsPositionInsideField(cardEntity.Get<CardData>().Position);

        private void SetCombinationCard(int cardEntity) => 
            cardEntity.Set<CombinationCard>();
    }
}