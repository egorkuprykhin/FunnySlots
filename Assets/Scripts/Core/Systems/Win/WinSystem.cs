using System.Collections.Generic;
using FunnySlots.Sound;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace FunnySlots
{
    public class WinSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<CardInsideField, CardData>> _cardsInsideField;

        private EcsCustomInject<FieldPositionsService> _positionService;
        private EcsCustomInject<Configuration> _configuration;
        private EcsCustomInject<CoreFactory> _coreFactory;

        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            bool hasWinCombinations = false;
            
            foreach (var cardInsideField in _cardsInsideField.Value)
            {
                ref var cardData = ref cardInsideField.Get<CardData>(_world);
                
                var horizontalCards = _positionService.Value.GetAllHorizontalCardsByCard(cardInsideField);
                var id = cardInsideField.Get<CardData>(_world).InitialData.Id;

                if (CheckCombinations(horizontalCards, cardData, id) > 0)
                    hasWinCombinations = true;
            }
            
            if (hasWinCombinations)
                PlayWinSound();
        }
        
        private void PlayWinSound()
        {
            ref var playSoundEvent = ref _world.NewEntity().Get<PlaySoundEvent>(_world);
            playSoundEvent.Type = CoreSound.Win;
            playSoundEvent.NeedPlay = true;
        }

        private int CheckCombinations(List<int> cardsCombination, CardData cardData, string id)
        {
            int combinations = 0;
            foreach (var combinationCard in cardsCombination)
            {
                ref var combinationCardData = ref combinationCard.Get<CardData>(_world);
                if (combinationCardData.Position == cardData.Position)
                    continue;

                if (WinnerCard(id, combinationCard))
                {
                    combinations++;

                    CardWinFrameView instance = _coreFactory.Value.CreateCardWinFrame(combinationCardData.Position);
                    
                    AddViewRef(combinationCard, instance);
                    SendAddScoresEvent();
                }
            }

            return combinations;
        }

        private void AddViewRef(int combinationCard, CardWinFrameView instance)
        {
            combinationCard.Get<CardWinFrameViewRef>(_world).Value = instance;
        }

        private void SendAddScoresEvent() => 
            _world.NewEntity().Get<AddScoresEvent>(_world).Value = _configuration.Value.CombinationScore;

        private bool WinnerCard(string id, int combinationCard)
        {
            return combinationCard.Has<CardInsideField>(_world)
                   &&
                   combinationCard.Get<CardData>(_world).InitialData.Id == id
                   &&
                   !combinationCard.Has<CardWinFrameViewRef>(_world);
        }
    }
}
