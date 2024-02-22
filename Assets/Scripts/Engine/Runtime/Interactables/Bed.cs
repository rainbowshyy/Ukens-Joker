using System.Collections.Generic;
using UkensJoker.DataArchitecture;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UkensJoker.Engine
{
    public class Bed : MonoBehaviour, IInteractable
    {
        [SerializeField] private IntVariable _day;
        [SerializeField] private IntVariable _money;
        [SerializeField] private IntReference _neededMoney;
        [SerializeField] private BudgetElement[] _BSU;

        [SerializeField] private UnityEvent _onEnough;
        [SerializeField] private UnityEvent _onNotEnough;

        private void Start()
        {
            if (_money.Value >= _neededMoney.Value)
                _onEnough.Invoke();
            else
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

        public void EnoughMoney(Component sender, object enough)
        {
            if (enough is bool)
            {
                if ((bool)enough)
                    _onEnough.Invoke();
                else
                    _onNotEnough.Invoke();
            }
        }
    }
}
