using UkensJoker.DataArchitecture;
using UnityEngine;

namespace UkensJoker.Engine
{
    public class SpotInterestEmitter : MonoBehaviour
    {
        [SerializeField] private RøvernSpot[] _røvernSpots;

        [SerializeField] private bool _spread;
        [SerializeField] private FloatReference _interest;
        [SerializeField] private FloatReference _interestSpreadMultiplier;

        public void EmitInterest()
        {
            for (int s = 0; s < _røvernSpots.Length; s++)
            {
                if (_røvernSpots[s] == null)
                {
                    Debug.LogWarning("No insterest spot set!");
                    continue;
                }
                _røvernSpots[s].AddInterest(_interest.Value);
                Debug.Log($"Added {_interest.Value} interest to {_røvernSpots[s].name}");
                if (_spread)
                {
                    for (int i = 0; i < _røvernSpots[s].Connections.Length; i++)
                    {
                        _røvernSpots[s].Connections[i].AddInterest(_interest.Value * _interestSpreadMultiplier.Value);
                        Debug.Log($"...spread {_interest.Value * _interestSpreadMultiplier.Value} interest to {_røvernSpots[s].Connections[i].name}");
                    }
                }
            }
        }
    }
}
