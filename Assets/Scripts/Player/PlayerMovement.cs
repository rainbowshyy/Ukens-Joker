using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Variables")]

    [SerializeField] private FloatReference _playerSpeed;
    [SerializeField] private FloatReference _playerDrag;
    [SerializeField] private FloatReference _playerAcceleration;
    private float _currentSpeed = 0f;

    [Header("Current Player Position")]
    [SerializeField] private Vector3Variable _playerPosition;

    private float horizontalInput;
    private float verticalInput;

    [Header("Move Input Vector")]
    [SerializeField] private Vector2Variable _moveInput;

    private Rigidbody rb;

    [SerializeField] private UnityEvent OnStartMove;
    [SerializeField] private UnityEvent OnStopMove;
    private bool _moving = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _playerPosition.Value = transform.position;
        rb.drag = _playerDrag.Value;
    }

    private void Update()
    {
        Input();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        if (rb.velocity.magnitude > 0.1f && !_moving)
        {
            _moving = true;
            OnStartMove.Invoke();
        }
        else if (rb.velocity.magnitude <= 0.1f && _moving)
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
            UpdatePlayerPosition();

            _currentSpeed = 0f;

            return;
        }

        _currentSpeed = Mathf.Clamp(_currentSpeed + _playerAcceleration.Value, 0f, (_playerSpeed.Value * movement.magnitude));

        rb.velocity = movement * _currentSpeed;

        _playerPosition.Value = transform.position;
    }

    private void UpdatePlayerPosition()
    {
        _playerPosition.Value = transform.position;
    }
}