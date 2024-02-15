using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.Engine
{
    public class Fisk : MonoBehaviour
    {
        [SerializeField] private UnityEvent _startPlay;
        [SerializeField] private UnityEvent _stopPlay;
        [SerializeField] private Animator _animator;

        private void OnTriggerEnter(Collider other)
        {
            _startPlay.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            _stopPlay.Invoke();
        }

        public void SetAnimation(bool active)
        {
            _animator.SetBool("Active", active);
        }
    }
}
