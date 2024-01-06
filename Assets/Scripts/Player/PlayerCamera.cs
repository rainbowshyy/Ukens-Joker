using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private float _xRotation;
    private float _yRotation;

    [Header("Move Input Vector")]
    [SerializeField] private Vector2Variable _lookInput;

    [Header("Player Direction Vector")]
    [SerializeField] private Vector3Variable _playerDirection;

    [Header("Look Sensitivity Variables")]
    [SerializeField] private FloatReference _playerLookSensitivityX;
    [SerializeField] private FloatReference _playerLookSensitivityY;

    [SerializeField] private Rigidbody rb;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        float mouseX = _lookInput.Value.x * Time.deltaTime * _playerLookSensitivityX.Value;
        float mouseY = _lookInput.Value.y * Time.deltaTime * _playerLookSensitivityY.Value;

        _yRotation += mouseX;
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        rb.MoveRotation(Quaternion.Euler(0, _yRotation, 0));

        _playerDirection.Value = transform.forward;
    }
}