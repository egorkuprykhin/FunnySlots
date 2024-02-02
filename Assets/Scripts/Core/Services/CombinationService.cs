using System.Collections.Generic;
using System.Linq;

namespace FunnySlots
{
    public class CombinationService
    {
        private List<CardData> _combinationCards = new();

        public void RegisterCombinationCard(CardData cardData) => 
            _combinationCards.Add(cardData);

        public string GetWinCombinationId()
        {
            var _combinations = GetCombinations();

            string MaxCombinationId = 
                _combinations.First( 
                    combination => combination.Value == _combinations.Values.Max())
                    .Key ;

            return MaxCombinationId;
        }

        public void ClearRegisteredCards()
        {
            _combinationCards.Clear();
        }

        private Dictionary<string, int> GetCombinations()
        {
            Dictionary<string, int> _combinations = new();

            foreach (CardData combinationCard in _combinationCards)
            {
                _combinations.TryAdd(combinationCard.InitialData.Id, 0);
                _combinations[combinationCard.InitialData.Id]++;
            }

            return _combinations;
        }
    }
}