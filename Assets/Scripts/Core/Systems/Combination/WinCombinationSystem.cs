using FunnySlots.Sound;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class WinCombinationSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<WinCombinationIdEvent>> _winCombinationIdEvent;
        private EcsFilterInject<Inc<StartRollEvent>> _startRollEvent;
        
        private EcsFilterInject<Inc<CombinationCard, CardData>> _combinationCards;

        private EcsCustomInject<Configuration> _configuration;
        private EcsCustomInject<CoreFactory> _coreFactory;

        private EcsWorldInject _world;


        public void Run(IEcsSystems systems)
        {
            foreach (int eventEntity in _winCombinationIdEvent.Value)
                RunWinCombinationFlow(eventEntity);
            
            foreach (int eventEntity in _startRollEvent.Value)
                RunStartRollFlow(eventEntity);
        }

        private void RunWinCombinationFlow(int eventEntity)
        {
            CreateWinFrames(eventEntity.Get<WinCombinationIdEvent>().WinnerId);
            PlayWinSound();
            
            _world.Delete<WinCombinationIdEvent>(eventEntity);
        }

        private void RunStartRollFlow(int eventEntity)
        {
            ClearCombinationCards();
            _world.Delete<StartRollEvent>(eventEntity);
        }

        private void PlayWinSound()
        {
            ref var playSoundEvent = ref _world.NewEntity().Get<SoundEvent>();
            playSoundEvent.Type = SoundEventType.Win;
            playSoundEvent.NeedPlay = true;
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

        private bool CardIsWinner(int cardEntity, string winnerId) => 
            cardEntity.Get<CardData>().InitialData.Id == winnerId;

        private void CreateCardWinFrameView(int combinationCard)
        {
            Vector2 position = combinationCard.Get<CardData>().Position;
            CardWinFrameView instance = _coreFactory.Value.Create<CardWinFrameView, Vector2>(position);
            
            combinationCard.Get<CardWinFrameViewRef>().Value = instance;
        }
        
        private void ClearCombinationCards()
        {
            foreach (int cardEntity in _combinationCards.Value)
            {
                if (cardEntity.Has<CardWinFrameViewRef>())
                {
                    Object.Destroy(cardEntity.Get<CardWinFrameViewRef>().Value.GameObject);
                    cardEntity.Del<CardWinFrameViewRef>();
                }
                
                cardEntity.Del<CombinationCard>();
            }
        }

        private void AddScoresForCard() => 
            _world.Create<AddScoresEvent>().Value = _configuration.Value.CombinationScore;
    }
}
