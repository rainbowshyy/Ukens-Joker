using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UkensJoker.Engine
{
    public class PlayerInput : MonoBehaviour
    {

        private PlayerControls _input;

        [SerializeField] private Vector2Variable _moveInput;
        [SerializeField] private Vector2Variable _lookInput;
        [SerializeField] private GameEvent _interactTriggered;
        [SerializeField] private GameEvent _interactReleased;
        [SerializeField] private GameEvent _backspaceTriggered;
        [SerializeField] private GameEvent _enterTriggered;
        [SerializeField] private GameEvent _numberPressed;

        public bool InteractValue { get; private set; }
        public float LongJump { get; private set; }

        private void Awake() { _input = new PlayerControls(); }

        private void Update()
        {
            _moveInput.Value = _input.Player.Move.ReadValue<Vector2>();

            //lookInput.Value = _input.Player.Look.ReadValue<Vector2>();
            Vector2 rawLookInput = _input.Player.Look.ReadValue<Vector2>();
            float aspectRatio = (float)Screen.width / Screen.height;

            // Adjust sensitivity based on aspect ratio
            Vector2 normalizedLookInput = new Vector2(rawLookInput.x / Screen.width * aspectRatio, rawLookInput.y / Screen.height) * 100f;
            _lookInput.Value = normalizedLookInput;
        }

        private void InteractInput(InputAction.CallbackContext context)
        {
            _interactTriggered.Raise(this, null);
        }

        private void InteractReleaseInput(InputAction.CallbackContext context)
        {
            _interactReleased.Raise(this, null);
        }

        private void NumberInput(int number)
        {
            _numberPressed.Raise(this, number);
        }

        private void BackspaceInput(InputAction.CallbackContext context)
        {
            _backspaceTriggered.Raise(this, null);
        }

        private void EnterInput(InputAction.CallbackContext context)
        {
            _enterTriggered.Raise(this, null);
        }

        private void OnEnable()
        {
            _input.Enable();
            _input.Player.Interact.started += InteractInput;
            _input.Player.Interact.canceled += InteractReleaseInput;
            _input.Player.Backspace.started += BackspaceInput;
            _input.Player.Enter.started += EnterInput;
            _input.Player._0.started += (e) => { NumberInput(0); };
            _input.Player._1.started += (e) => { NumberInput(1); };
            _input.Player._2.started += (e) => { NumberInput(2); };
            _input.Player._3.started += (e) => { NumberInput(3); };
            _input.Player._4.started += (e) => { NumberInput(4); };
            _input.Player._5.started += (e) => { NumberInput(5); };
            _input.Player._6.started += (e) => { NumberInput(6); };
            _input.Player._7.started += (e) => { NumberInput(7); };
            _input.Player._8.started += (e) => { NumberInput(8); };
            _input.Player._9.started += (e) => { NumberInput(9); };
        }
        private void OnDisable() { _input.Disable(); }
    }
}