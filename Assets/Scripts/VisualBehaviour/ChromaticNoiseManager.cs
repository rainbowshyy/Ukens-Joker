using System.Collections.Generic;
using UkensJoker.DataArchitecture;
using UnityEngine;

namespace UkensJoker.VisualBehaviour
{
    public class ChromaticNoiseManager : MonoBehaviour
    {
        [SerializeField] private Material _aberrationMat;
        [SerializeField] private FloatReference _chromaticNoiseMultiplier;
        [SerializeField] private FloatReference _chromaticNoiseDistanceMultiplier;
        [SerializeField] private Vector3Reference _playerPosition;

        private Dictionary<int, ChromaticNoise> _chromaticNoiseMap = new Dictionary<int, ChromaticNoise>();

        public void ReceiveChromaticNoise(Component sender, object chromaticNoise)
        {
            if (chromaticNoise is not ChromaticNoise)
            {
                Debug.LogWarning("Parameter is not of type Chromatic Noise!");
                return;
            }

            ChromaticNoise cn = new ChromaticNoise((ChromaticNoise) chromaticNoise);

            if (!_chromaticNoiseMap.ContainsKey(cn.Hash))
                _chromaticNoiseMap.Add(cn.Hash, cn);
            else
                Debug.LogWarning("Dictionary already contains hash!");
        }

        public void RemoveChromaticNoise(Component sender, object hash)
        {
            if (hash is not int)
            {
                Debug.LogWarning("Parameter is not of type int!");
                return;
            }

            _chromaticNoiseMap.Remove((int) hash);
        }

        private void Update()
        {
            float totalValue = 0f;

            List<int> keysToRemove = new List<int>();

            foreach (int key in _chromaticNoiseMap.Keys)
            {
                if (_chromaticNoiseMap[key].Continuous)
                {
                    totalValue += _chromaticNoiseMap[key].BaseValue * Mathf.Clamp(_chromaticNoiseMultiplier.Value - (_chromaticNoiseMap[key].Position - _playerPosition.Value).magnitude * _chromaticNoiseDistanceMultiplier.Value, 0f, 10f);
                }
                else
                {
                    totalValue += _chromaticNoiseMap[key].BaseValue * _chromaticNoiseMultiplier.Value;
                    _chromaticNoiseMap[key].BaseValue -= _chromaticNoiseMap[key].DecayValue * Time.deltaTime;
                    if (_chromaticNoiseMap[key].BaseValue < 0f)
                        keysToRemove.Add(key);
                }
            }

            foreach (int key in keysToRemove)
            {
                _chromaticNoiseMap.Remove(key);
            }

            _aberrationMat.SetFloat("_Noise", totalValue);
        }
    }
}
