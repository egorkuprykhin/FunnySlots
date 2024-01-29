namespace FunnySlots
{
    public class CardsSpriteSelectorService 
    {
        private readonly CardsData _cardsData;
        
        public CardsSpriteSelectorService(CardsData cardsData)
        {
            _cardsData = cardsData;
        }

        public CardInitializeData GetRandomCardEntryData()
        {
            var randomIndex = UnityEngine.Random.Range(0, _cardsData.Cards.Count);
            return _cardsData.Cards[randomIndex];
        }
    }
}