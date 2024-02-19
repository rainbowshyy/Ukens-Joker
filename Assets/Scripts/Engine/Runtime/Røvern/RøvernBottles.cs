using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.Engine
{
    public class RøvernBottles : MonoBehaviour
    {
        [SerializeField] private FloatReference _røvernBottlesFrequency;
        [SerializeField] private FloatReference _røvernBottlesChance;

        [SerializeField] private UnityEvent _onRøvernBottles;

        private float _røvernBottlesTimeCurrent;

        private void OnTriggerStay(Collider other)
        {
            _røvernBottlesTimeCurrent += Time.deltaTime;

            if (_røvernBottlesTimeCurrent >= _røvernBottlesFrequency.Value)
            {
                _røvernBottlesTimeCurrent -= _røvernBottlesFrequency.Value;
                if (Random.Range(0f, 1f) < _røvernBottlesChance.Value)
                {
                    _onRøvernBottles.Invoke();
                }
            }
        }
    }
}
