using System.Collections.Generic;
using FunnySlots.Sound;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class WinRollSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<CardInsideField, CardData>> _cardsInsideField;

        private EcsCustomInject<FieldPositionsService> _positionService;
        private EcsCustomInject<Configuration> _configuration;
        
        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            bool playWinSound = false;
            foreach (var cardInsideField in _cardsInsideField.Value)
            {
                ref var cardData = ref cardInsideField.Get<CardData>(_world);
                
                var horizontalCards = _positionService.Value.GetAllHorizontalCardsByCard(cardInsideField);
                var verticalCards = _positionService.Value.GetAllHorizontalCardsByCard(cardInsideField);
                var id = cardInsideField.Get<CardData>(_world).InitialData.Id;

                if (CheckCombinations(horizontalCards, cardData, id) > 0)
                    playWinSound = true;
                if (CheckCombinations(verticalCards, cardData, id) > 0)
                    playWinSound = true;
            }
            
            if(playWinSound)
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

                if (combinationCard.Has<CardInsideField>(_world)
                    &&
                    combinationCard.Get<CardData>(_world).InitialData.Id == id
                    &&
                    !combinationCard.Has<WinFrameViewRef>(_world))
                {
                    combinations++;
                    
                    CardWinFrameView prefab = _configuration.Value.CardWinFrameView;
                    var position = combinationCardData.Position;
                    
                    CardWinFrameView winViewInstance = Object.Instantiate(prefab, position, Quaternion.identity);
                    combinationCard.Get<WinFrameViewRef>(_world).Value = winViewInstance;
                    
                    _world.NewEntity().Get<AddScoresEvent>(_world).Value = _configuration.Value.CombinationScore;
                }
            }

            return combinations;
        }
    }
}
