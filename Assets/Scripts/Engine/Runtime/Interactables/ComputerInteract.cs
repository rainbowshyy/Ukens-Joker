using Cinemachine;
using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.Engine
{
    public class ComputerInteract : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _interactText;

        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private GameEvent _setPlayerControls;

        [SerializeField] private UnityEvent OnUseComputer;
        [SerializeField] private UnityEvent OnStopComputer;

        private bool _usingComputer;

        public string GetInteractText()
        {
            return _usingComputer ? "" : _interactText;
        }

        public void Interact(Vector3 direction)
        {
            _setPlayerControls.Raise(this, _usingComputer);

            _usingComputer = !_usingComputer;

            _virtualCamera.Priority = _usingComputer ? 100 : 0;

            if (_usingComputer)
                OnUseComputer?.Invoke();
            else
                OnStopComputer?.Invoke();
        }
    }
}
