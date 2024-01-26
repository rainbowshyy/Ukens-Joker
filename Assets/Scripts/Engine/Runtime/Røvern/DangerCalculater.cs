using UnityEngine;
using UkensJoker.DataArchitecture;

namespace UkensJoker.Engine
{
    public class DangerCalculater : MonoBehaviour
    {
        [SerializeField] private FloatVariable _danger;

        [SerializeField] private FloatReference _sightDanger;

        private void OnEnable()
        {
            _sightDanger.RegisterListener(CalculateDanger);
        }

        private void OnDisable()
        {
            _sightDanger.UnregisterListener(CalculateDanger);
        }

        private void CalculateDanger(float nothing)
        {
            _danger.Value = _sightDanger.Value;
        }
    }
}
