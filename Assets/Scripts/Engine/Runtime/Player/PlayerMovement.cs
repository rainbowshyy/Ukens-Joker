using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.Engine
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement Variables")]

        [SerializeField] private FloatReference _playerSpeed;
        [SerializeField] private FloatReference _playerDrag;
        [SerializeField] private FloatReference _playerAcceleration;
        private float _currentSpeed = 0f;

        private float horizontalInput;
        private float verticalInput;

        [Header("Move Input Vector")]
        [SerializeField] private Vector2Variable _moveInput;

        private Rigidbody rb;

        [SerializeField] private UnityEvent OnStartMove;
        [SerializeField] private UnityEvent OnStopMove;

        [SerializeField] private FloatReference _footstepFrequency;
        [SerializeField] private UnityEvent _onFootstep;
        private float _footstepTimeCurrent;

        private bool _moving = false;

        private bool _canMove = true;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.drag = _playerDrag.Value;
        }

        private void Update()
        {
            Input();
            if (_moving)
            {
                _footstepTimeCurrent += Time.deltaTime;
                if (_footstepTimeCurrent > _footstepFrequency.Value)
                {
                    _footstepTimeCurrent -= _footstepFrequency.Value;
                    _onFootstep.Invoke();
                }
            }
        }

        private void FixedUpdate()
        {
            if (!_canMove)
                return;

            MovePlayer();
            if (rb.velocity.magnitude > 0.5f && !_moving)
            {
                _moving = true;
                _footstepTimeCurrent = _footstepFrequency.Value;
                OnStartMove.Invoke();
            }
            else if (rb.velocity.magnitude <= 0.5f && _moving)
            {
                _moving = false;
                OnStopMove.Invoke();
            }
        }
        private void Input()
        {
            horizontalInput = _moveInput.Value.x;
            verticalInput = _moveInput.Value.y;
        }

        private void MovePlayer()
        {
            Vector3 movement = (transform.forward * verticalInput + transform.right * horizontalInput).normalized;

            if (movement.magnitude == 0)
            {
                _currentSpeed = 0f;

                return;
            }

            _currentSpeed = Mathf.Clamp(_currentSpeed + _playerAcceleration.Value, 0f, _playerSpeed.Value * movement.magnitude);

            rb.velocity = movement * _currentSpeed;
        }

        public void PlayerControlsChanged(Component sender, object enabled)
        {
            if (enabled is bool)
            {
                _canMove = (bool)enabled;
                rb.isKinematic = !(bool)enabled;
                GetComponent<Collider>().enabled = _canMove;
                if (!(bool)enabled && _moving)
                {
                    _moving = false;
                    OnStopMove.Invoke();
                }
            }
        }
    }
}