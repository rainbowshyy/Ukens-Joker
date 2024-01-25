using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UkensJoker.DataArchitecture;

namespace UkensJoker.Engine
{
    public class RøvernMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;

        [SerializeField] private Vector3Reference _playerPosition;

        private void Update()
        {
            _agent.SetDestination(_playerPosition.Value);
        }
    }
}
