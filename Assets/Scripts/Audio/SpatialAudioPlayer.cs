namespace UkensJoker.Audio
{
    public class SpatialAudioPlayer : AudioPlayer
    {
        protected override void Awake()
        {
            base.Awake();
            _audioSource.spatialBlend = 1f;
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
