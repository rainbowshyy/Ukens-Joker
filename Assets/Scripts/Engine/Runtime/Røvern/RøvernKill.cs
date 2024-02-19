using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UkensJoker.Engine
{
    public class RøvernKill : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onKill;

        private void OnTriggerEnter(Collider other)
        {
            _onKill.Invoke();
            SceneManager.LoadScene(0);
        }
    }
}
