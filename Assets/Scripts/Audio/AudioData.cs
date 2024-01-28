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
    }
}
