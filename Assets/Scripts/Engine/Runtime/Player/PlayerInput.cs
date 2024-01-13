using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UkensJoker.Engine
{
    public class PlayerInput : MonoBehaviour
    {

        private PlayerControls _input;

        [SerializeField] Vector2Variable moveInput;
        [SerializeField] Vector2Variable lookInput;
        [SerializeField] GameEvent interactTriggered;
        [SerializeField] GameEvent interactReleased;

        public bool InteractValue { get; private set; }
        public float LongJump { get; private set; }

        private void Awake() { _input = new PlayerControls(); }

        private void Update()
        {
            moveInput.Value = _input.Player.Move.ReadValue<Vector2>();

            //lookInput.Value = _input.Player.Look.ReadValue<Vector2>();
            Vector2 rawLookInput = _input.Player.Look.ReadValue<Vector2>();
            float aspectRatio = (float)Screen.width / Screen.height;

            // Adjust sensitivity based on aspect ratio
            Vector2 normalizedLookInput = new Vector2(rawLookInput.x / Screen.width * aspectRatio, rawLookInput.y / Screen.height) * 100f;
            lookInput.Value = normalizedLookInput;
        }

        private void InteractInput(InputAction.CallbackContext context)
        {
            interactTriggered.Raise();
        }

        private void InteractReleaseInput(InputAction.CallbackContext context)
        {
            interactReleased.Raise();
        }

        private void OnEnable()
        {
            _input.Enable();
            _input.Player.Interact.started += InteractInput;
            _input.Player.Interact.canceled += InteractReleaseInput;
        }
        private void OnDisable() { _input.Disable(); }
    }
}