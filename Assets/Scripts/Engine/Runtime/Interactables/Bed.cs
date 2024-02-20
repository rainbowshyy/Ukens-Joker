using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UkensJoker.Engine
{
    public class Bed : MonoBehaviour, IInteractable
    {
        [SerializeField] private IntVariable _day;

        private void Awake()
        {
            this.enabled = false;
        }

        public string GetInteractText()
        {
            return "Go to sleep";
        }

        public void Interact(Vector3 direction)
        {
            _day.Value += 1;
            SceneManager.LoadScene(1);
        }
    }
}
