using UnityEngine;

namespace FunnySlots
{
    public class CoreFactory
    {
        public CoreFactory(Configuration configuration) => 
            InitFactories(configuration);

        private void InitFactories(Configuration configuration)
        {
            Factory<MaskView>.Instance = new MaskFactory(configuration);

            FactoryWithPayload<CardView, CardData>.Instance = new CardFactory(configuration);
            FactoryWithPayload<ScoresView, Transform>.Instance = new ScoresFactory(configuration);
            FactoryWithPayload<CardWinFrameView, Vector2>.Instance = new CardWinFrameFactory(configuration);
        }

        public TView Create<TView>() where TView : CoreView => 
            Factory<TView>.Instance.Create();

        public TView Create<TView, TPayload>(TPayload payload) where TView : CoreView => 
            FactoryWithPayload<TView, TPayload>.Instance.Create(payload);
    }
}