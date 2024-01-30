namespace FunnySlots.Sound
{
    public struct PlaySoundEvent : IComponent
    {
        public CoreSound Type;
        public bool NeedPlay;
    }
}