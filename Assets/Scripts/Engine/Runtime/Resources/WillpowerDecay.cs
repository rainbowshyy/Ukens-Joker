using UkensJoker.DataArchitecture;
using UnityEngine;

namespace UkensJoker.Engine
{
    public class WillpowerDecay : MonoBehaviour
    {
        [SerializeField] private FloatVariable _willpower;
        [SerializeField] private FloatReference _willpowerMax;
        [SerializeField] private FloatReference _willpowerDecay;

        private void Update()
        {
            if (_willpower.Value < 0)
            {
                _willpower.Value = 0;
                return;
            }
            _willpower.Value = Mathf.Clamp(_willpower.Value - _willpowerDecay.Value * Time.deltaTime, 0f, _willpowerMax.Value);
        }
    }
}
