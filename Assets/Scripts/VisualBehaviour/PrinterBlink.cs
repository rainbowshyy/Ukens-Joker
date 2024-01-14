using UnityEngine;
using UkensJoker.DataArchitecture;

namespace UkensJoker.VisualBehaviour
{
    public class PrinterBlink : MonoBehaviour
    {
        [SerializeField] private GameObject[] _lights;
        [SerializeField] private FloatReference _printerBlinkFrequency;

        private float _timeCurrent;
        private int _blinkNumber;

        private void Update()
        {
            _timeCurrent += Time.deltaTime;
            if (_timeCurrent >= _printerBlinkFrequency.Value)
            {
                _timeCurrent -= _printerBlinkFrequency.Value;
                _lights[_blinkNumber].gameObject.SetActive(false);
                _blinkNumber = (_blinkNumber + 1) % 2;
                _lights[_blinkNumber].gameObject.SetActive(true);
            }
        }
    }
}
