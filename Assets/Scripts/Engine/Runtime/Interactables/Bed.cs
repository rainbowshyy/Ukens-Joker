using System.Collections.Generic;
using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UkensJoker.Engine
{
    public class Bed : MonoBehaviour, IInteractable
    {
        [SerializeField] private IntVariable _day;
        [SerializeField] private IntVariable _money;
        [SerializeField] private IntReference _neededMoney;
        [SerializeField] private BudgetElement[] _BSU;

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
            _money.Value += _BSU[_day.Value].Bought ? Mathf.Abs(_BSU[_day.Value].Delta) - _neededMoney.Value : -_neededMoney.Value;
            _day.Value += 1;
            SceneManager.LoadScene(1);
        }
    }
}
