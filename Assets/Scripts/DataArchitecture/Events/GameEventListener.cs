using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.DataArchitecture
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent GameEvent;
        public CustomUnityEvent Response;

        private void OnEnable()
        {
            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            GameEvent.RemoveListener(this);
        }

        public virtual void OnEventRaised(Component sender, object data)
        {
            Response.Invoke(sender, data);
        }
    }
}