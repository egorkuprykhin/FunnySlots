namespace FunnySlots
{
    public struct PlayingSound : IComponent
    {
        public float LifeTime;
        public float ElapsedTime;
        
        public AudioSourceView AudioSourceView;
    }
}