using System.Collections;
using System.Collections.Generic;
using UkensJoker.DataArchitecture;
using UnityEngine;

namespace UkensJoker.VisualBehaviour
{
    public class DangerEffects : MonoBehaviour
    {
        [SerializeField] private bool _UIFailSafe;

        [SerializeField] private FloatReference _danger;
        [SerializeField] private FloatReference _willpower;
        [SerializeField] private FloatReference _willpowerMax;
        [SerializeField] private Vector2Reference _sightPosition;

        [Header("Material References")]
        [SerializeField] private Material _dangerMat;
        [SerializeField] private Material _aberrationMat;
        [SerializeField] private Material _ditherMat;

        [Header("Default values")]
        [SerializeField] private FloatReference _ditherBlackThresholdDefault;

        [Header("Multipliers")]
        [SerializeField] private FloatReference _dangerThresholdMultiplier;
        [SerializeField] private FloatReference _aberrationPixelMultiplier;
        [SerializeField] private FloatReference _ditherBlackThresholdMultiplier;
        [SerializeField] private FloatReference _willpowerDitherBlackMultipler;

        private bool _clamp;

        private void Start()
        {
            if (_danger.Variable != null)
                SetMaterialValues(0f);
            else
                SetMaterialValues(_danger.Value);
            SetWillpowerValues(_willpower.Value);
        }

        private void Update()
        {
            if (_UIFailSafe)
            {
                _aberrationMat.SetFloat("_Noise", 0f);
                _aberrationMat.SetFloat("_Danger", _clamp ? Mathf.Clamp(0.99f + _aberrationPixelMultiplier.Value * _danger.Value, 0f, 2f) : 0.99f + _aberrationPixelMultiplier.Value * _danger.Value);
            }
        }

        private void OnEnable()
        {
            if (_danger.Variable != null)
                _danger.RegisterListener(SetMaterialValues);
            if (_willpower.Variable != null)
                _willpower.RegisterListener(SetWillpowerValues);
            if (_sightPosition.Variable != null)
                _sightPosition.RegisterListener(SetSightPosition);
            else
                SetSightPosition(_sightPosition.Value);
        }

        private void OnDisable()
        {
            if (_danger.Variable != null)
                _danger.UnregisterListener(SetMaterialValues);
            if (_willpower.Variable != null)
                _willpower.UnregisterListener(SetWillpowerValues);
            if (_sightPosition.Variable != null)
                _sightPosition?.UnregisterListener(SetSightPosition);
        }

        private void OnValidate()
        {
            SetMaterialValues(_danger.Value);
        }

        private void SetWillpowerValues(float value)
        {
            _ditherMat.SetFloat("_Willpower", (1f - value / _willpowerMax.Value) * _willpowerDitherBlackMultipler.Value);
        }

        private void SetMaterialValues(float value)
        {
            _dangerMat.SetFloat("_Threshold", 10f - (1f - Mathf.Pow(1f - value, 5)) * _dangerThresholdMultiplier.Value);
            _aberrationMat.SetFloat("_Danger", _clamp ? Mathf.Clamp(0.99f + _aberrationPixelMultiplier.Value * value, 0f, 2f) :  0.99f + _aberrationPixelMultiplier.Value * value);
            _ditherMat.SetFloat("_BlackThreshold", _ditherBlackThresholdDefault.Value + value * _ditherBlackThresholdMultiplier.Value);
        }

        private void SetSightPosition(Vector2 value)
        {
            _dangerMat.SetVector("_RovernPosition", value);
            _aberrationMat.SetVector("_RovernPosition", value);
        }

        public void ClampAberration(bool clamp)
        {
            _clamp = clamp;
        }
    }
}
