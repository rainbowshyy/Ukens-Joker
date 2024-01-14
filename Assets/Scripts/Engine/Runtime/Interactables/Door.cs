using UnityEngine;

namespace UkensJoker.Engine
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _openDoorInteractText, _closedDoorInteractText;

        private bool _openDoor = false;

        public string GetInteractText()
        {
            return _openDoor ? _openDoorInteractText : _closedDoorInteractText;
        }

        public void Interact()
        {
            _openDoor = !_openDoor;
        }
    }
}
