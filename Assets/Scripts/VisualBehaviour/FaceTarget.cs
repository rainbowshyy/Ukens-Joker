using UnityEngine;

namespace UkensJoker.VisualBehaviour
{
    public class FaceTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void Update()
        {
            transform.forward = target.position - transform.position;
        }
    }
}