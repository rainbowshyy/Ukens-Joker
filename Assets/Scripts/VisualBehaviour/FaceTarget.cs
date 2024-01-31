using UnityEngine;
using UkensJoker.DataArchitecture;

namespace UkensJoker.VisualBehaviour
{
    public class FaceTarget : MonoBehaviour
    {
        [SerializeField] private Vector3Reference _target;
        [SerializeField] private Vector3 _offset;

        private void Update()
        {
            transform.forward = _target.Value + _offset - transform.position;
        }
    }
}