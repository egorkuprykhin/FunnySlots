using System;
using UnityEngine;

namespace FunnySlots
{
    [Serializable]
    public class SoundDataEntry
    {
        public SoundEventType Event;
        public AudioClip[] Clips;
    }
}