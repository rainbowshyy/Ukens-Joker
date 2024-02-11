using System.Collections;
using UnityEngine;

namespace UkensJoker.Audio
{
    public abstract class AudioPlayer : MonoBehaviour
    {
        [SerializeField] protected AudioData _audio;

        protected AudioSource[] _audioSources;

        protected int _lastPlayedIndex = 0;

        protected virtual void Awake()
        {
            if (!_audio.CanOverlap)
                _audioSources = new AudioSource[] { gameObject.AddComponent<AudioSource>() };
            else
            {
                _audioSources = new AudioSource[2];
                _audioSources[0] = gameObject.AddComponent<AudioSource>();
                _audioSources[1] = gameObject.AddComponent<AudioSource>();
            }

            if (_audio is not RandomAudioData)
            {
                for (int i = 0; i < _audioSources.Length; i++)
                {
                    _audioSources[i].loop = _audio.Loop;
                }
            }

            SetAudioData();

            if (_audio.PlayOnAwake)
                Play();
        }

        protected void SetAudioData()
        {
            _audioSources[_lastPlayedIndex].clip = _audio.Clip;
            _audioSources[_lastPlayedIndex].volume = _audio.Volume;
            _audioSources[_lastPlayedIndex].pitch = _audio.Pitch;
        }

        public virtual void Play()
        {
            _lastPlayedIndex = _audio.CanOverlap ? 1 - _lastPlayedIndex : 0;
            if (_audio is RandomAudioData)
            {
                SetAudioData();
                if (_audio.Loop)
                    StartCoroutine(SetAudioDataIfLooped());
            }
            _audioSources[_lastPlayedIndex].Play();
        }

        public virtual void Stop()
        {
            for (int i = 0; i < _audioSources.Length; i++)
            {
                _audioSources[i].Stop();
            }
            StopAllCoroutines();
        }

        private IEnumerator SetAudioDataIfLooped()
        {
            if (!_audioSources[_lastPlayedIndex].isPlaying)
            {
                SetAudioData();
                _audioSources[_lastPlayedIndex].Play();
            }

            yield return new WaitForEndOfFrame();
            StartCoroutine(SetAudioDataIfLooped());
        }
    }
}
