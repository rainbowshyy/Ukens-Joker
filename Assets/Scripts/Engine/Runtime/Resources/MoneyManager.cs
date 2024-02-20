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

        private bool _hasBroadcasted;

        private void Update()
        {
            if (_money.Value >= _moneyMax.Value && !_hasBroadcasted)
            {
                _hasBroadcasted = true;
                _enoughMoney.Raise(this, null);
            }
        }
    }
}
