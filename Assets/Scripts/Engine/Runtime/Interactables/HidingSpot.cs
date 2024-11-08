using UnityEngine;
using Cinemachine;
using UkensJoker.DataArchitecture;
using UnityEngine.Events;

namespace UkensJoker.Engine
{
    public class HidingSpot : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _hidingSpotNamePrompt;

        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private GameEvent _setPlayerControls;
        [SerializeField] private GameEvent _setHiding;
        [SerializeField] private Transform _røvernFoundLocation;
        [SerializeField] private GameEvent _setHidingLocation;

        [SerializeField] private UnityEvent OnStartHide;
        [SerializeField] private UnityEvent OnStopHide;

        private bool _hiding;

        public string GetInteractText()
        {
            return _hiding ? "Stop hiding" : _hidingSpotNamePrompt;
        }

        public void Interact(Vector3 direction)
        {
            _setPlayerControls.Raise(this, _hiding);

            _hiding = !_hiding;

            if (_hiding)
                OnStartHide?.Invoke();
            else
                OnStopHide?.Invoke();

            _setHiding.Raise(this, _hiding);
            _setHidingLocation.Raise(this, _røvernFoundLocation.position);

            _virtualCamera.Priority = _hiding ? 100 : 0;
        }
    }
}
