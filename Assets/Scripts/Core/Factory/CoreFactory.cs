using UnityEngine;

namespace FunnySlots
{
    public class CoreFactory
    {
        private readonly Configuration _configuration;

        public CoreFactory(Configuration configuration) => 
            _configuration = configuration;

        public MaskView CreateMask()
        {
            MaskView instance = 
                Object.Instantiate(_configuration.MaskView);
            
            instance.transform.localScale = new Vector3(
                _configuration.FieldSize.x + 1, 
                _configuration.FieldSize.y, 
                1);

            return instance;
        }

        public CardView CreateCard(CardData cardData)
        {
            CardView instance = 
                Object.Instantiate(_configuration.CardView, cardData.Position, Quaternion.identity);
            
            instance.CardRenderer.sprite = cardData.InitialData.Sprite;

            return instance;
        }
        
        public CardWinFrameView CreateCardWinFrame(Vector2 position)
        {
            CardWinFrameView instance = 
                Object.Instantiate(_configuration.CardWinFrameView, position, Quaternion.identity);
            
            return instance;
        }

        public ScoreView CreateScoresView(Transform parent)
        {
            ScoreView instance = 
                Object.Instantiate(_configuration.ScoreView, parent);

            return instance;
        }
    }
}