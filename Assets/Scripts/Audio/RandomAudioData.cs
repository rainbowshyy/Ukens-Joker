using UnityEngine;

namespace UkensJoker.Audio
{
    [CreateAssetMenu(menuName = "Audio/Create Random Audio", fileName = "RandomAudioData")]
    public class RandomAudioData : AudioData
    {
        [SerializeField] private AudioClip[] _audioClips;
        [SerializeField] [Range(0f, 2f)] private float _pitchMin;
        [SerializeField] [Range(0f, 2f)] private float _pitchMax;
        [SerializeField] [Range(0f, 2f)] private float _volumeMin;
        [SerializeField] [Range(0f, 2f)] private float _volumeMax;

        public override AudioClip Clip
        {
            get { return _audioClips[Random.Range(0, _audioClips.Length)]; }
        }

        public override float Volume
        {
            get { return Random.Range(_volumeMin, _volumeMax); }
        }

        public override float Pitch
        {
            get { return Random.Range(_pitchMin, _pitchMax); }
        }
    }
}
