using UkensJoker.DataArchitecture;
using UnityEngine;

namespace UkensJoker.Engine
{
    public class BreakerBox : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _powerOnText;
        [SerializeField] private string _powerOffText;

        [SerializeField] private FloatReference _powerOffChance;
        [SerializeField] private FloatReference _powerOffCheckFrequency;
        [SerializeField] private FloatReference _powerOffWillpowerPenaltyMultiplier;
        [SerializeField] private FloatReference _willpower;
        [SerializeField] private FloatReference _willpowerMax;

        [SerializeField] private GameEvent _powerChanged;

        private bool _power = true;
        private float _timeCurrent;

        private void Update()
        {
            if (!_power)
                return;

            _timeCurrent += Time.deltaTime;
            if (_timeCurrent > _powerOffCheckFrequency.Value)
            {
                _timeCurrent -= _powerOffCheckFrequency.Value;
                if (Random.Range(0f, 1f) < _powerOffChance.Value + _powerOffWillpowerPenaltyMultiplier.Value * (1f - _willpower.Value / _willpowerMax.Value))
                {
                    _powerChanged.Raise(this, false);
                    _power = false;
                }
            }
        }

        public string GetInteractText()
        {
            return _power ? _powerOnText : _powerOffText;
        }

        public void Interact(Vector3 direction)
        {
            if (_power)
                return;

            _powerChanged.Raise(this, true);
            _power = true;
            _timeCurrent = 0f;
        }
    }
}
