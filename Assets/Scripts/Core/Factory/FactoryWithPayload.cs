namespace FunnySlots
{
    public static class FactoryWithPayload<TView, TPayload> where TView : CoreView
    {
        public static IFactoryWithPayload<TView, TPayload> Instance;
    }
}