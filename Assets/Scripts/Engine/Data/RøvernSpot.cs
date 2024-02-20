using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UkensJoker.Engine
{
    [CreateAssetMenu(menuName = "Create Røvern Spot", fileName ="RøvernSport")]
    public class RøvernSpot : ScriptableObject
    {
        [HideInInspector] public Vector3 Position;
        [field: SerializeField] public RøvernSpot[] Connections { get; private set; }
        [field: SerializeField] public bool PathfindingEnabled { get; private set; }

        [HideInInspector] public bool HasPlayer;

        public float Interest { get; private set; }

        public void AddInterest(float interest)
        {
            Interest += interest;
            OnInterestChanged?.Invoke(interest);
            for (int i = 0; i < Connections.Length; i++)
            {
                Connections[i].OnInterestChanged?.Invoke(interest);
            }
        }    

        public void SetInterest(float interest)
        {
            Interest = interest;
        }

        public Action<float> OnInterestChanged;
    }
}
