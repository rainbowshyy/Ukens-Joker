using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UkensJoker.DataArchitecture
{
    public class SetVector3Position : MonoBehaviour
    {
        [SerializeField] private Vector3Variable _variable;

        private void Update()
        {
            if (_variable != null)
            {
                _variable.Value = transform.position;
            }
        }
    }
}
