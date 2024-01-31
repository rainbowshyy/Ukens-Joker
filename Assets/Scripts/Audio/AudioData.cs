using System;
using UnityEngine;

namespace UkensJoker.Audio
{
    public abstract class AudioData : ScriptableObject
    {
        public bool Loop;
        public bool PlayOnAwake;

        public virtual AudioClip Clip { get; }
        public virtual float Volume { get; }
        public virtual float Pitch { get; }

        public AudioData() { }

        [Serializable]
        public class ReverbData
        {
            [Range(-10000, 0)] public float Room = -1000;
            [Range(-10000, 0)] public float RoomHF = -454;
            [Range(-10000, 0)] public float RoomLF = 0;
            [Range(0.1f, 20f)] public float DecayTime = 0.4f;
            [Range(0.1f, 20f)] public float DecayHFRatio = 0.83f;
            [Range(-10000, 1000)] public float ReflectionsLevel = -1646;
            [Range(0f, 0.3f)] public float ReflectionsDelay = 0;
            [Range(-10000, 2000)] public float ReverbLevel = -53;
            [Range(0f, 0.1f)] public float ReverbDelay = 0.003f;
            [Range(1000, 20000)] public float HFReference = 5000;
            [Range(20, 1000)] public float LFReference = 250;
            [Range(0, 100)] public float Diffusion = 100;
            [Range(0, 100)] public float Density = 100;
        }

        public ReverbData Reverb;
    }
}
