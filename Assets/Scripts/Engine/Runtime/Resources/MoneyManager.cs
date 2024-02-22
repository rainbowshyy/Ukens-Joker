using System.Collections;
using System.Collections.Generic;
using UkensJoker.DataArchitecture;
using UnityEngine;

namespace UkensJoker.Engine
{
    public class MoneyManager : MonoBehaviour
    {
        [SerializeField] private IntReference _money;
        [SerializeField] private IntReference _moneyMax;
        [SerializeField] private GameEvent _enoughMoney;

        private void OnEnable()
        {
            _money.RegisterListener(OnNewMoney);
        }

        private void OnDisable()
        {
            _money.UnregisterListener(OnNewMoney);
        }

        private void OnNewMoney(int value)
        {
            _enoughMoney.Raise(this, value >= _moneyMax.Value);
        }
    }
}
