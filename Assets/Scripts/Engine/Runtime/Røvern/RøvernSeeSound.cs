using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.Events;

namespace UkensJoker.Engine
{
    public class RøvernSeeSound : MonoBehaviour
    {
        [SerializeField] private FloatReference _danger;

        [SerializeField] private UnityEvent<float> _onNewDanger;

        private void OnEnable()
        {
            _danger.RegisterListener(DoNewDanger);
        }

        private void OnDisable()
        {
            _danger.UnregisterListener(DoNewDanger);
        }

        private void DoNewDanger(float value)
        {
            _onNewDanger.Invoke(value);
        }
    }
}
