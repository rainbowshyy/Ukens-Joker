using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UkensJoker.Engine
{
    public class PlayerInRøvernSpot : MonoBehaviour
    {
        [SerializeField] private RøvernSpot _room;

        private void Awake()
        {
            _room.HasPlayer = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            _room.HasPlayer = true;
        }

        private void OnTriggerExit(Collider other)
        {
            _room.HasPlayer = false;
        }
    }
}
