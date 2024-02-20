using UkensJoker.DataArchitecture;
using UnityEngine;

namespace UkensJoker.Engine
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private Vector3Variable _playerDirection;
        [SerializeField] private FloatReference _playerInteractionRange;
        [SerializeField] private StringVariable _interactText;

        [SerializeField] private LayerMask Interactable;

        private IInteractable _interactableCurrent;

        private bool _canInteract = true;

        private void Update()
        {
            if (_interactableCurrent == null)
                _interactText.Value = "";
            else
                _interactText.Value = _interactableCurrent.GetInteractText();

            if (!_canInteract)
                return;

            _interactableCurrent = GetInteractable();
        }

        private IInteractable GetInteractable()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, _playerDirection.Value, out hit, _playerInteractionRange.Value, Interactable))
            {
                IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
                return interactable;
            }
            return null;
        }

        public void TryInteract()
        {
            if (_interactableCurrent == null)
                return;
            _interactableCurrent.Interact(_playerDirection.Value);
        }

        public void PlayerControlsChanged(Component sender, object enabled)
        {
            if (enabled is bool)
                _canInteract = (bool)enabled;
        }
    }
}
