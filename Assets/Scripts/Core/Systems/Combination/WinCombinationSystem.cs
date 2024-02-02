using FunnySlots.Sound;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class WinCombinationSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<WinCombinationIdEvent>> _winCombinationIdEvent;
        
        private EcsFilterInject<Inc<CombinationCard, CardData>> _combinationCards;

        private EcsCustomInject<Configuration> _configuration;
        private EcsCustomInject<CoreFactory> _coreFactory;

        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (int eventEntity in _winCombinationIdEvent.Value)
                WinCombinationFlow(eventEntity);
        }

        private void WinCombinationFlow(int eventEntity)
        {
            CreateWinFrames(eventEntity.Get<WinCombinationIdEvent>().WinnerId);
            PlayWinSound();
            
            eventEntity.Delete<WinCombinationIdEvent>();
        }


        private void CreateWinFrames(string winnerId)
        {
            foreach (int cardEntity in _combinationCards.Value)
                
                if (CardIsWinner(cardEntity, winnerId))
                {
                    CreateCardWinFrameView(cardEntity);
                    AddScoresForCard();
                }
        }

        private void PlayWinSound() => 
            _world.Create<PlaySoundEvent>().EventType = SoundEventType.Win;

        private bool CardIsWinner(int cardEntity, string winnerId) => 
            cardEntity.Get<CardData>().InitialData.Id == winnerId;

        private void CreateCardWinFrameView(int combinationCard)
        {
            Vector2 position = combinationCard.Get<CardData>().Position;
            CardWinFrameView instance = _coreFactory.Value.Create<CardWinFrameView, Vector2>(position);
            
            combinationCard.Get<CardWinFrameViewRef>().Value = instance;
        }
        
        private void AddScoresForCard() => 
            _world.Create<AddScoresEvent>().Value = _configuration.Value.CombinationScore;
    }
}
