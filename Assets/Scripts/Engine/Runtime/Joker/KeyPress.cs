using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.Engine
{
    public class KeyPress : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onKeyPress;

        public void DoKeyPress()
        {
            _onKeyPress.Invoke();
        }
    }
}
