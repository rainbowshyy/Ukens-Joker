using UkensJoker.DataArchitecture;
using UnityEngine;

namespace UkensJoker.Engine
{
    public class WillpowerDecay : MonoBehaviour
    {
        [SerializeField] private FloatVariable _willpower;
        [SerializeField] private FloatReference _willpowerMax;
        [SerializeField] private FloatReference _willpowerDecay;

        private void Awake()
        {
            _willpower.Value = _willpowerMax.Value;
        }

        private void Update()
        {
            _willpower.Value -= _willpowerDecay.Value * Time.deltaTime;
        }
    }
}
