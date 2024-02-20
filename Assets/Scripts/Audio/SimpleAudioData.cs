using UnityEngine;

namespace UkensJoker.Audio
{
    [CreateAssetMenu(menuName = "Audio/Create Simple Audio", fileName = "SimpleAudioData")]
    public class SimpleAudioData : AudioData
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField] private float _volume;
        [SerializeField] private float _pitch;

        public override AudioClip Clip { get { return _clip; } }
        public override float Volume { get { return _volume; } }
        public override float Pitch { get { return _pitch; } }
    }
}
