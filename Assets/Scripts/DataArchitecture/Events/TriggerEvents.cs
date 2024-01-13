using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.DataArchitecture
{
    public class TriggerEvents : MonoBehaviour
    {
        [SerializeField] private UnityEvent<GameObject> _onTriggerEnterGameobject;
        [SerializeField] private UnityEvent<GameObject> _onTriggerExitGameobject;
        [SerializeField] private UnityEvent _onTriggerEnter;
        [SerializeField] private UnityEvent _onTriggerExit;

        private void OnTriggerEnter(Collider other)
        {
            if (!enabled)
                return;
            _onTriggerEnter.Invoke();
            _onTriggerEnterGameobject.Invoke(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            _onTriggerExit.Invoke();
            _onTriggerExitGameobject.Invoke(other.gameObject);
        }

        public void DestroyGameobject(GameObject gameObject)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}