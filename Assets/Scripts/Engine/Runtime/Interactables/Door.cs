using UnityEngine;
using UkensJoker.DataArchitecture;
using System;
using UnityEngine.Events;

namespace UkensJoker.Engine
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private string _openDoorInteractText, _closedDoorInteractText;

        [Header("Values")]
        [SerializeField] private FloatReference _doorOpenTime;

        [SerializeField] private UnityEvent OnOpenDoor;
        [SerializeField] private UnityEvent OnCloseDoor;

        private float _yRotDefault;
        private float _yRotPrevious;
        private float _yRotTarget;
        private bool _isTransitioning;
        private float _transitionTime;


        private bool _openDoor = false;

        private void Awake()
        {
            _yRotDefault = transform.eulerAngles.y;
        }

        private void Update()
        {
            if (!_isTransitioning)
                return;

            _transitionTime += (Time.deltaTime / _doorOpenTime.Value) * (_openDoor ? 1f : 1.5f);

            _transform.eulerAngles = new Vector3(0f, Mathf.LerpAngle(_yRotPrevious, _yRotTarget, -(Mathf.Cos(Mathf.PI * _transitionTime) - 1f) / 2f), 0f);

            if (_transitionTime >= 1f)
            {
                _isTransitioning = false;
                if (!_openDoor)
                    OnCloseDoor?.Invoke();
            }
        }

        public string GetInteractText()
        {
            return _openDoor ? _openDoorInteractText : _closedDoorInteractText;
        }

        public void Interact(Vector3 direction)
        {
            if (_isTransitioning)
                return;

            if (!_openDoor)
                OnOpenDoor?.Invoke();

            _openDoor = !_openDoor;
            _isTransitioning = true;
            _transitionTime = 0f;
            _yRotPrevious = _transform.eulerAngles.y;

            if (_openDoor)
            {
                float openDirection = Mathf.Sign(Vector3.Dot(direction, transform.forward));
                _yRotTarget = _yRotDefault + openDirection * 90f;
            }
            else
            {
                _yRotTarget = _yRotDefault;
            }
        }
    }
}
