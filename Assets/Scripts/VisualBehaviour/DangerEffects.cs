using System.Collections;
using System.Collections.Generic;
using UkensJoker.DataArchitecture;
using UnityEngine;

namespace UkensJoker.VisualBehaviour
{
    public class DangerEffects : MonoBehaviour
    {
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

        private void Awake()
        {
            SetMaterialValues(0f);
            SetWillpowerValues(_willpower.Value);
        }

        private void OnEnable()
        {
            _danger.RegisterListener(SetMaterialValues);
            _willpower.RegisterListener(SetWillpowerValues);
            _sightPosition.RegisterListener(SetSightPosition);
        }

        private void OnDisable()
        {
            _danger.UnregisterListener(SetMaterialValues);
            _willpower.UnregisterListener(SetWillpowerValues);
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
            _aberrationMat.SetFloat("_Danger", 0.99f + _aberrationPixelMultiplier.Value * value);
            _ditherMat.SetFloat("_BlackThreshold", _ditherBlackThresholdDefault.Value + value * _ditherBlackThresholdMultiplier.Value);
        }

        private void SetSightPosition(Vector2 value)
        {
            _dangerMat.SetVector("_RovernPosition", value);
            _aberrationMat.SetVector("_RovernPosition", value);
        }
    }
}
