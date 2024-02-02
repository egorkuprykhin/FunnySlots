using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

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
            TryInitHighestCards();

            foreach (var highestCardInRow in _highestCards.Value) 
                SelectHighestCardInRow(highestCardInRow);
        }

        private void SelectHighestCardInRow(int currentHighestCardInRow)
        {
            var row = currentHighestCardInRow.Get<HighestCardInRow>().Row;
            int potentialHighestCardInRow = GetCardWithMaxYPositionInRow(row);

            if (potentialHighestCardInRow != currentHighestCardInRow)
            {
                currentHighestCardInRow.Delete<HighestCardInRow>();
                potentialHighestCardInRow.Get<HighestCardInRow>().Row = row;
            }
        }

        private void TryInitHighestCards()
        {
            if (_highestCards.Value.GetEntitiesCount() > 0)
                return;
            
            int rowsCount = _configuration.Value.FieldSize.x;

            for (int row = 0; row < rowsCount; row ++)
            {
                int highestCardInRowByPosition = GetCardWithMaxYPositionInRow(row);
                
                highestCardInRowByPosition.Get<HighestCardInRow>().Row = 
                    highestCardInRowByPosition.Get<CardData>().Row;
            }
        }

        private int GetCardWithMaxYPositionInRow(int row)
        {
            float cardMaxPosY = float.MinValue;
            int entity = -1;
            
            foreach (int cardEntity in _cards.Value)
            {
                ref var cardData = ref cardEntity.Get<CardData>();
                float cardPosY = cardData.Position.y;

                if (cardData.Row == row && cardPosY > cardMaxPosY)
                {
                    cardMaxPosY = cardPosY;
                    entity = cardEntity;
                }
            }

            return entity;
        }
    }
    
}