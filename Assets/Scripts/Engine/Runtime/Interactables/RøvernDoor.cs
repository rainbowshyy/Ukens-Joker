using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.Engine
{
    public class RøvernDoor : MonoBehaviour
    {
        [SerializeField] private UnityEvent<Vector3> _onRøvernEnter;

        private void OnTriggerEnter(Collider other)
        {
            _onRøvernEnter.Invoke(transform.position - other.transform.position);
        }
    }
}
