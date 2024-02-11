using UnityEngine;

namespace UkensJoker.Audio
{
    public class GlobalAudioPlayer : AudioPlayer
    {
        protected override void Awake()
        {
            base.Awake();
            for (int i = 0; i < _audioSources.Length; i++)
            {
                _audioSources[i].spatialBlend = 0f;
            }
        }

        public override void Play()
        {
            base.Play();
        }

        public override void Stop()
        {
            base.Stop();
        }
    }
}
