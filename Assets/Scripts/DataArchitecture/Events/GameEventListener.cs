using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.DataArchitecture
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent GameEvent;
        public UnityEvent Response;

        private void OnEnable()
        {
            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            GameEvent.RemoveListener(this);
        }

        public virtual void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}