using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UkensJoker.Engine
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _openDoorInteractText, _closedDoorInteractText;
        private bool _openDoor = false;

        private string _interactTextCurrent;

        public string InteractText { get { return _interactTextCurrent; } }

        private void Awake()
        {
            SetInteractText();
        }

        private void SetInteractText()
        {
            _interactTextCurrent = _openDoor ? _openDoorInteractText : _closedDoorInteractText;
        }

        public void Interact()
        {
            _openDoor = !_openDoor;
            SetInteractText();
        }
    }
}
