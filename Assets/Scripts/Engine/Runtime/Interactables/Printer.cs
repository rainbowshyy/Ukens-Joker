using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.Engine
{
    public class Printer : MonoBehaviour, IInteractable
    {
        [SerializeField] private UnityEvent _onStartPrint;
        [SerializeField] private UnityEvent _onCompletePrint;

        [SerializeField] private string _startPrintText;
        [SerializeField] private string _printingText;

        private bool _printing;

        [SerializeField] private FloatReference _printTime;
        private float _printTimeCurrent;

        public string GetInteractText()
        {
            return _printing ? _printingText : _startPrintText;
        }

        public void Interact(Vector3 direction)
        {
            if (_printing)
                return;

            _printing = true;
            _onStartPrint.Invoke();
        }

        private void Update()
        {
            if (!_printing)
                return;

            _printTimeCurrent += Time.deltaTime;

            if (_printTimeCurrent >= _printTime.Value)
            {
                _printTimeCurrent = 0;
                _printing = false;

                _onCompletePrint.Invoke();
            }
        }
    }
}
