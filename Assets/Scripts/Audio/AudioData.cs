using UnityEngine;

namespace UkensJoker.Audio
{
    public abstract class AudioData : ScriptableObject
    {
        public virtual AudioClip Clip { get; }
        public virtual float Volume { get; }
        public virtual float Pitch { get; }

        public AudioData() { }

        public AudioData(AudioClip audio, float volume, float pitch)
        {
            Clip = audio;
            Volume = volume;
            Pitch = pitch;
        }
    }
}
