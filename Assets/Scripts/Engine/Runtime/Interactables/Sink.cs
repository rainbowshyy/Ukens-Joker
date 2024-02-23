using System.Collections;
using System.Collections.Generic;
using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.Engine
{
    public class Sink : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _normalText;
        [SerializeField] private string _brokenText;

        [SerializeField] private IntReference _day;
        [SerializeField] private FloatReference _willpower;
        [SerializeField] private FloatReference _willpowerMax;
        [SerializeField] private IntVariable _money;
        [SerializeField] private FloatReference _breakChance;
        [SerializeField] private FloatReference _willpowerBreakChanceMultiplier;
        [SerializeField] private FloatReference _breakCheckFrequency;
        [SerializeField] private FloatReference _loseMoneyFrequency;

        [SerializeField] private GameEvent _sinkBroken;

        [SerializeField] private UnityEvent _onBreak;
        [SerializeField] private UnityEvent _onFix;

        private bool _broken;

        private float _timeCurrent;

        private void Update()
        {
            if (_day.Value < 2)
                return;

            if (_broken)
            {
                _timeCurrent += Time.deltaTime;
                if (_timeCurrent > _loseMoneyFrequency.Value)
                {
                    _timeCurrent -= _loseMoneyFrequency.Value;
                    _money.Value -= 1;
                }
                return;
            }

            _timeCurrent += Time.deltaTime;
            if (_timeCurrent > _breakCheckFrequency.Value)
            {
                _timeCurrent -= _breakCheckFrequency.Value;
                if (Random.Range(0f, 1f) < _breakChance.Value + _willpowerBreakChanceMultiplier.Value * (1f - _willpower.Value / _willpowerMax.Value))
                {
                    _broken = true;
                    _onBreak.Invoke();
                    _sinkBroken.Raise(this, true);
                }
            }

        }

        public string GetInteractText()
        {
            return _broken ? _brokenText : _normalText;
        }

        public void Interact(Vector3 direction)
        {
            if (!_broken)
                return;

            _broken = false;
            _onFix.Invoke();
            _sinkBroken.Raise(this, false);
        }
    }
}
