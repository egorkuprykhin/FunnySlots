namespace FunnySlots
{
    public static class FactoryWithPayloadInstanceProxy<TView, TPayload> where TView : CoreView
    {
        public static IFactoryWithPayload<TView, TPayload> Instance;
    }
}