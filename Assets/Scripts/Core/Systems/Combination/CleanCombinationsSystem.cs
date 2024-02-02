using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class CleanCombinationsSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<StartRollEvent>> _startRollEvent;
        
        private EcsFilterInject<Inc<CombinationCard, CardData>> _combinationCards;


        public void Run(IEcsSystems systems)
        {
            foreach (int eventEntity in _startRollEvent.Value)
                ClearCombinationCards();
        }

        private void ClearCombinationCards()
        {
            foreach (int cardEntity in _combinationCards.Value)
            {
                if (cardEntity.Has<CardWinFrameViewRef>())
                {
                    Object.Destroy(cardEntity.Get<CardWinFrameViewRef>().Value.GameObject);
                    cardEntity.Delete<CardWinFrameViewRef>();
                }
                
                cardEntity.Delete<CombinationCard>();
            }
        }
    }
}