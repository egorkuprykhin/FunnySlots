namespace FunnySlots
{
    public class CardsInitializeDataService 
    {
        private readonly CardsData _cardsData;
        
        public CardsInitializeDataService(CardsData cardsData)
        {
            _cardsData = cardsData;
        }

        public CardInitializeData GetRandomCardInitializeData()
        {
            var randomIndex = UnityEngine.Random.Range(0, _cardsData.Cards.Count);
            return _cardsData.Cards[randomIndex];
        }
    }
}