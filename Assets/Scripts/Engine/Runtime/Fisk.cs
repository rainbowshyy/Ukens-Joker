using System.Collections.Generic;
using System.Linq;
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

        private HashSet<Collider> _colliders = new HashSet<Collider>();

        private void Update()
        {
            if (_playing && GetActiveColliderCount() <= 0)
            {
                _stopPlay.Invoke();
                _playing = false;
            }
            else if (!_playing && GetActiveColliderCount() > 0)
            {
                _startPlay.Invoke();
                _playing = true;
                _playTimeCurrent = 0;
            }

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
            if (!_colliders.Contains(other))
            {
                _colliders.Add(other);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_colliders.Contains(other))
            {
                _colliders.Remove(other);
            }
        }

        private int GetActiveColliderCount()
        {
            if (_colliders.Count == 0 ) return 0;
            return _colliders.Where(x => x.enabled).Count();
        }

        public void SetAnimation(bool active)
        {
            _animator.SetBool("Active", active);
        }
    }
}
