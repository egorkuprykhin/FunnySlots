using UnityEngine;

namespace FunnySlots
{
    public class CardFactory : IFactoryWithPayload<CardView, CardData>
    {
        private readonly Configuration _configuration;

        public CardFactory(Configuration configuration) => 
            _configuration = configuration;
        
        public CardView Create(CardData cardData)
        {
            CardView instance = Object.Instantiate(_configuration.CardView, cardData.Position, Quaternion.identity);
            
            instance.CardRenderer.sprite = cardData.InitialData.Sprite;

            return instance;
        }
    }
}