using System.Collections;
using UnityEngine;

namespace UkensJoker.Audio
{
    public abstract class AudioPlayer : MonoBehaviour
    {
        [SerializeField] protected AudioData _audio;

        protected AudioSource _audioSource;

        protected virtual void Awake()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();

            if (_audio is not RandomAudioData)
                _audioSource.loop = _audio.Loop;

            _audioSource.playOnAwake = _audio.PlayOnAwake;

            SetAudioData();
        }

        protected void SetAudioData()
        {
            _audioSource.clip = _audio.Clip;
            _audioSource.volume = _audio.Volume;
            _audioSource.pitch = _audio.Pitch;
        }

        public virtual void Play()
        {
            if (_audio is RandomAudioData)
            {
                SetAudioData();
                if (_audio.Loop)
                    StartCoroutine(SetAudioDataIfLooped());
            }
            _audioSource.Play();
        }

        public virtual void Stop()
        {
            _audioSource.Stop();
            StopAllCoroutines();
        }

        private IEnumerator SetAudioDataIfLooped()
        {
            if (!_audioSource.isPlaying)
            {
                SetAudioData();
                _audioSource.Play();
            }

            yield return new WaitForEndOfFrame();
            StartCoroutine(SetAudioDataIfLooped());
        }
    }
}
