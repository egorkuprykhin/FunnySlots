namespace FunnySlots
{
    public static class Factory<TView> where TView : CoreView
    {
        public static IFactory<TView> Instance;
    }
}