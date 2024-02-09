using System;
using UnityEngine;

namespace FunnySlots
{
    [Serializable]
    public class SoundDataEntry
    {
        public SoundEventType Event;
        public AudioClipData[] AudioClipsData;
    }

    [Serializable]
    public class AudioClipData
    {
        public AudioClip AudioClip;
        public float SoundLength;
        [Range(0, 1)] public float Volume;
    }
}