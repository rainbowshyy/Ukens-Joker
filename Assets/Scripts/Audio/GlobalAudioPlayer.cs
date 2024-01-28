using UnityEngine;

namespace UkensJoker.Audio
{
    public class GlobalAudioPlayer : AudioPlayer
    {
        protected override void Awake()
        {
            base.Awake();
            _audioSource.spatialBlend = 0f;
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
