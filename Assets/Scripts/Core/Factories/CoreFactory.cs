namespace FunnySlots
{
    public class CoreFactory
    {
        public CoreFactory(Configuration configuration) => 
            InitFactories(configuration);

        private void InitFactories(Configuration configuration)
        {
            InitFactory(new MaskFactory(configuration));
            
            InitFactoryWithPayload(new CardFactory(configuration));
            InitFactoryWithPayload(new ScoresFactory(configuration));
            InitFactoryWithPayload(new CardWinFrameFactory(configuration));
        }

        public TView Create<TView>() where TView : CoreView => 
            FactoryInstanceProxy<TView>.Instance.Create();

        public TView Create<TView, TPayload>(TPayload payload) where TView : CoreView => 
            FactoryWithPayloadInstanceProxy<TView, TPayload>.Instance.Create(payload);
        
        private void InitFactory<TView>(IFactory<TView> factory) where TView : CoreView=>
            FactoryInstanceProxy<TView>.Instance = factory;
        
        private void InitFactoryWithPayload<TView, TPayload>(IFactoryWithPayload<TView, TPayload> factory) where TView : CoreView =>
            FactoryWithPayloadInstanceProxy<TView, TPayload>.Instance = factory;
    }
}