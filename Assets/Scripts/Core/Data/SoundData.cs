using System;
using System.Collections.Generic;
using UnityEngine;

namespace FunnySlots
{
    [Serializable]
    public class SoundData
    {
        public List<SoundDataEntry> Sounds;
    }

    [Serializable]
    public class SoundDataEntry
    {
        public CoreSound Event;
        public AudioClip[] Clips;
    }
}