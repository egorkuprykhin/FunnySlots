using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace FunnySlots
{
    public class WatchHighestCardSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<CardData>> _cards;
        private EcsFilterInject<Inc<HighestCardInRow>> _highestCards;
        
        private EcsCustomInject<Configuration> _configuration;
        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            if (_highestCards.Value.GetEntitiesCount() == 0)
                InitHighestCards();

            foreach (var highestCardInRow in _highestCards.Value)
            {
                var row = highestCardInRow.Get<HighestCardInRow>(_world).Row;
                int potentialHighestCardInRow = GetCardWithMaxYPositionInRow(row);

                if (potentialHighestCardInRow != highestCardInRow)
                {
                    highestCardInRow.Del<HighestCardInRow>(_world);
                    potentialHighestCardInRow.Get<HighestCardInRow>(_world).Row = row;
                }
            }
        }

        private void InitHighestCards()
        {
            int rowsCount = _configuration.Value.FieldSize.x;
            Debug.Log($"Init highest cards rows count: {rowsCount}");

            for (int row = 0; row < rowsCount; row ++)
            {
                int highestCardInRowByPosition = GetCardWithMaxYPositionInRow(row);
                
                highestCardInRowByPosition.Get<HighestCardInRow>(_world).Row = 
                    highestCardInRowByPosition.Get<CardData>(_world).Row;
            }
        }
        
        private int GetCardWithMaxYPositionInRow(int row)
        {
            float cardMaxPosY = float.MinValue;
            int entity = -1;
            
            foreach (int cardEntity in _cards.Value)
            {
                ref var cardData = ref cardEntity.Get<CardData>(_world);
                float cardPosY = cardData.Position.y;

                if (cardData.Row == row && cardPosY > cardMaxPosY)
                {
                    cardMaxPosY = cardPosY;
                    entity = cardEntity;
                }
            }

            return entity;
        }

        private bool ApproximatelyEqual(float cardPosY, float cardMaxPosY) => 
            Mathf.Abs(cardPosY - cardMaxPosY) < _configuration.Value.Epsilon;
    }
    
}