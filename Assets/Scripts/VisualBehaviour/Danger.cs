using System.Collections;
using System.Collections.Generic;
using UkensJoker.DataArchitecture;
using UnityEngine;

namespace UkensJoker.VisualBehaviour
{
    public class Danger : MonoBehaviour
    {
        [SerializeField] private Material _dangerMat;
        [SerializeField] private Material _aberrationMat;
        [SerializeField] private Material _ditherMat;

        [Header("Default values")]
        [SerializeField] private FloatReference _ditherBlackThresholdDefault;

        [Header("Multipliers")]
        [SerializeField] private FloatReference _dangerThresholdMultiplier;
        [SerializeField] private FloatReference _aberrationPixelMultiplier;
        [SerializeField] private FloatReference _ditherBlackThresholdMultiplier;

        [Range(0.0f, 1.0f)] [SerializeField] private float _dangerLevel;
        public float DangerLevel { 
            get
            {
                return _dangerLevel;
            }
            private set
            {
                _dangerLevel = value;
                _dangerMat.SetFloat("_Threshold", 10f - (1f - Mathf.Pow(1f - _dangerLevel, 5)) * _dangerThresholdMultiplier.Value);
                _aberrationMat.SetFloat("_PixelOffset", Mathf.Floor(0.99f + _aberrationPixelMultiplier.Value * _dangerLevel));
                _ditherMat.SetFloat("_BlackThreshold", _ditherBlackThresholdDefault.Value + _dangerLevel * _ditherBlackThresholdMultiplier.Value);
            }
        }

        private void OnValidate()
        {
            _dangerMat.SetFloat("_Threshold", 10f - (1f - Mathf.Pow(1f - _dangerLevel, 5)) * _dangerThresholdMultiplier.Value);
            _aberrationMat.SetFloat("_PixelOffset", Mathf.Floor(0.99f + _aberrationPixelMultiplier.Value * _dangerLevel));
            _ditherMat.SetFloat("_BlackThreshold", _ditherBlackThresholdDefault.Value + _dangerLevel * _ditherBlackThresholdMultiplier.Value);
        }
    }
}
