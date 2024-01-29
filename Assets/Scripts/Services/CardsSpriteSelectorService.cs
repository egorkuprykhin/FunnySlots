namespace FunnySlots
{
    public class CardsSpriteSelectorService 
    {
        private readonly CardsData _cardsData;
        
        public CardsSpriteSelectorService(Configuration configuration)
        {
            _cardsData = configuration.CardsData;
        }

        public CardEntry GetRandomCardEntryData()
        {
            var randomIndex = UnityEngine.Random.Range(0, _cardsData.Cards.Count);
            return _cardsData.Cards[randomIndex];
        }
    }
}