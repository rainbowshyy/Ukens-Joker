using UnityEngine;

namespace UkensJoker.Audio
{
    public class RøvernChaseSound : AudioPlayer
    {
        private float _baseVolume;

        private bool _stopping;

        protected override void Awake()
        {
            base.Awake();
            for (int i = 0; i < _audioSources.Length; i++)
            {
                _audioSources[i].spatialBlend = 0f;
            }

            _baseVolume = _audioSources[0].volume;
        }

        private void Update()
        {
            if (_audioSources[0].volume < _baseVolume && !_stopping)
            {
                _audioSources[0].volume = Mathf.Clamp(_audioSources[0].volume + Time.deltaTime, 0f, _baseVolume);
            }
            else if (_stopping && _audioSources[0].volume > 0f)
            {
                _audioSources[0].volume -= Time.deltaTime;
            }
        }

        public override void Play()
        {
            _audioSources[0].volume = 0f;
            _stopping = false;
            base.Play();

        }

        public override void Stop()
        {
            _stopping = true;
        }
    }
}
