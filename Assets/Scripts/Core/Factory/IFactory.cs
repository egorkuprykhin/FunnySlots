namespace FunnySlots
{
    public interface IFactory<out TView> where TView : CoreView
    {
        public TView Create();
    }
}