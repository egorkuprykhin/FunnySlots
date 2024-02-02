using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots
{
    public class SelectWinCombinationSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<SelectWinCombinationEvent>> _winCombinationEvent;
        private EcsFilterInject<Inc<CardData, CombinationCard>> _combinationCards;
        
        private EcsCustomInject<CombinationService> _combinationsService;

        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var winCombinationEvent in _winCombinationEvent.Value)
            {
                _world.Create<WinCombinationIdEvent>().WinnerId = CalculateWinCombinationId();
                _world.Delete<SelectWinCombinationEvent>(winCombinationEvent);
            }
        }
        
        private string CalculateWinCombinationId()
        {
            foreach (int winCombinationCard in _combinationCards.Value)
                _combinationsService.Value.RegisterCombinationCard(winCombinationCard.Get<CardData>());
            
            string maxCombinationId = _combinationsService.Value.GetWinCombinationId();

            _combinationsService.Value.ClearRegisteredCards();
            
            return maxCombinationId;
        }
    }
}