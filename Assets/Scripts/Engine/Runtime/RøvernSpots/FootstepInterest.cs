using UkensJoker.DataArchitecture;
using UnityEngine;

namespace UkensJoker.Engine
{
    public class FootstepInterest : MonoBehaviour
    {
        [SerializeField] private RøvernSpot[] _spots;
        
        [SerializeField] private FloatReference _footstepInterest;

        public void EmitInterest()
        {
            for (int i = 0; i < _spots.Length; i++)
            {
                if (_spots[i].HasPlayer)
                {
                    _spots[i].AddInterest(_footstepInterest.Value);
                    Debug.Log($"Added {_footstepInterest.Value} interest to {_spots[i].name}");
                }
            }
        }
    }
}
