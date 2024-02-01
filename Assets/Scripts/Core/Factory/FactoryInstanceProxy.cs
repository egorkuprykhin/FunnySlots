namespace FunnySlots
{
    public static class FactoryInstanceProxy<TView> where TView : CoreView
    {
        public static IFactory<TView> Instance;
    }
}