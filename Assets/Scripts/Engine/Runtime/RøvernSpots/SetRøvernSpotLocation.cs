using UkensJoker.Data;
using UnityEngine;

namespace UkensJoker.Engine
{
    public class SetRøvernSpotLocation : MonoBehaviour
    {
        [SerializeField] private RøvernSpot _røvernSpot;

        private void Awake()
        {
            _røvernSpot.Position = transform.position;
        }
    }
}
