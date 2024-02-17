using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.Engine
{
    public class Fisk : MonoBehaviour
    {
        [SerializeField] private UnityEvent _startPlay;
        [SerializeField] private UnityEvent _stopPlay;
        [SerializeField] private Animator _animator;

        private bool _playing;
        [SerializeField] private float _playTimeTick;
        private float _playTimeCurrent;
        [SerializeField] private UnityEvent _playTick;

        private void Update()
        {
            if (!_playing)
                return;

            _playTimeCurrent += Time.deltaTime;
            if (_playTimeCurrent >= _playTimeTick)
            {
                _playTimeCurrent -= _playTimeTick;
                _playTick.Invoke();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            _startPlay.Invoke();
            _playing = true;
            _playTimeCurrent = 0;
        }

        private void OnTriggerExit(Collider other)
        {
            _stopPlay.Invoke();
            _playing = false;
        }

        public void SetAnimation(bool active)
        {
            _animator.SetBool("Active", active);
        }
    }
}
