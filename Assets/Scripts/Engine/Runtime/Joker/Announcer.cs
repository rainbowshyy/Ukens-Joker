using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.Engine
{
    public class Announcer : MonoBehaviour
    {
        private bool _hasPlayed;

        [SerializeField] private UnityEvent _onPlayPasswordScreen;
        [SerializeField] private UnityEvent _onPlayJokerScreen;

        public void ResetHasPlayed()
        {
            _hasPlayed = false;
        }

        public void TryPlayPasswordScreen()
        {
            if (_hasPlayed)
                return;

            _hasPlayed = true;
            _onPlayPasswordScreen.Invoke();
        }

        public void TryPlayJokerScreen()
        {
            if (_hasPlayed)
                return;

            _hasPlayed = true;
            _onPlayJokerScreen.Invoke();
        }
    }
}
