using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.VisualBehaviour
{
    public class PlayerTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onEnter;
        [SerializeField] private UnityEvent _onLeave;
        [SerializeField] private UnityEvent _onStay;

        private void OnTriggerEnter(Collider other)
        {
            _onEnter.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            _onLeave.Invoke();
        }

        private void OnTriggerStay(Collider other)
        {
            _onStay.Invoke();
        }
    }
}
