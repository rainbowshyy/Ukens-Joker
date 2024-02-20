using UkensJoker.DataArchitecture;
using UnityEngine;

namespace UkensJoker.VisualBehaviour
{
    public class ChromaticNoiseEmitter : MonoBehaviour
    {
        [SerializeField] private GameEvent _chromaticNoise;
        [SerializeField] private GameEvent _chromaticNoiseStop;

        [SerializeField] private bool _continuous;
        [SerializeField] private float _baseValue;
        [SerializeField] private float _decayValue;

        private ChromaticNoise _noiseObject;

        private void Awake()
        {
            _noiseObject = new ChromaticNoise(_continuous, _baseValue, _decayValue, transform.position);
        }

        public void Emit()
        {
            _chromaticNoise.Raise(this, _noiseObject);
        }

        public void ForceStop()
        {
            _chromaticNoiseStop.Raise(this, _noiseObject.Hash);
        }
    }
}
