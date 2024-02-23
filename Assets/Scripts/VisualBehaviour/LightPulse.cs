using System.Collections;
using System.Collections.Generic;
using UkensJoker.DataArchitecture;
using UnityEngine;

namespace UkensJoker.VisualBehaviour
{
    public class LightPulse : MonoBehaviour
    {
        [SerializeField] private Light _light;

        [SerializeField] private FloatReference _pulseIntensity;
        [SerializeField] private FloatReference _pulseFrequency;

        private float _baseIntensity;
        private float _timeCurrent;

        private void Awake()
        {
            _baseIntensity = _light.intensity;
        }

        private void Update()
        {
            _timeCurrent += Time.deltaTime;

            _light.intensity = _baseIntensity + Mathf.Sin(_timeCurrent * _pulseFrequency.Value) * _pulseIntensity.Value;
        }
    }
}
