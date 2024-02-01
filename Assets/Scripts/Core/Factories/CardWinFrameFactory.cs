using UnityEngine;

namespace FunnySlots
{
    public class CardWinFrameFactory : IFactoryWithPayload<CardWinFrameView, Vector2>
    {
        private readonly Configuration _configuration;

        public CardWinFrameFactory(Configuration configuration) => 
            _configuration = configuration;
        
        public CardWinFrameView Create(Vector2 position)
        {
            CardWinFrameView instance = Object.Instantiate(_configuration.CardWinFrameView, position, Quaternion.identity);
            
            return instance;
        }
    }
}