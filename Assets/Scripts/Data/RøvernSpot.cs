using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UkensJoker.Data
{
    [CreateAssetMenu(menuName = "Create Røvern Spot", fileName ="RøvernSport")]
    public class RøvernSpot : ScriptableObject
    {
        [HideInInspector] public Vector3 Position;
        [field: SerializeField] public RøvernSpot[] Connections { get; private set; }
        [field: SerializeField] public bool PathfindingEnabled { get; private set; }

        [HideInInspector] public float Interest;
    }
}
