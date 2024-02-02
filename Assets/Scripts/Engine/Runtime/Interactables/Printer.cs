using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System;
using System.Linq;

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

        [SerializeField] private Transform _paperTransform;
        [SerializeField] private TMP_Text[] _paperTexts;
        [SerializeField] private float _paperStartPos;

        [SerializeField] private Transform _vibrateTransform;
        [SerializeField] private FloatReference _vibrateIntensity;
        [SerializeField] private FloatReference _vibrateFrequency;

        [SerializeField] private StringReference _password;

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

            _paperTexts[1].text = _paperTexts[0].text;
            _paperTexts[0].text = "LOGIN:\n" + String.Join(" ",_password.Value.ToCharArray()).Insert(16, "\n").Insert(8, "\n");
            _paperTransform.localPosition = new Vector3(0f, _paperStartPos, 0f);
        }

        private void Update()
        {
            if (!_printing)
                return;

            _printTimeCurrent += Time.deltaTime;

            _paperTransform.localPosition = new Vector3(0f, Mathf.Lerp(_paperStartPos, 0f, _printTimeCurrent / _printTime.Value), 0f);

            _vibrateTransform.localPosition = new Vector3(0f, _printTimeCurrent % _vibrateFrequency.Value < _vibrateFrequency.Value / 2f ? 0f : _vibrateIntensity.Value, 0f);

            if (_printTimeCurrent >= _printTime.Value)
            {
                _printTimeCurrent = 0;
                _printing = false;

                _vibrateTransform.localPosition = Vector3.zero;
                _onCompletePrint.Invoke();
            }
        }
    }
}
