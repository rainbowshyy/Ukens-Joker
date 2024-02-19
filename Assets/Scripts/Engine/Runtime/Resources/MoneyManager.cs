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
    }
}
