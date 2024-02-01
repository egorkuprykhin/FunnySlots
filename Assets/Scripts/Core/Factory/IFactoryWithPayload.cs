namespace FunnySlots
{
    public interface IFactoryWithPayload<out TView, in TPayload> where TView : CoreView
    {
        public TView Create(TPayload payload);
    }
}