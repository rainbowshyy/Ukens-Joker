using System;
using UnityEngine;

namespace UkensJoker.DataArchitecture
{
    [CreateAssetMenu(fileName = "BoolVar", menuName = "Variables/Bool")]
    public class BoolVariable : ScriptableObject
    {
        [SerializeField] private bool _bool;

        public event Action<bool> OnValueChanged;

        public bool Value
        {
            get { return _bool; }
            set
            {
                if (_bool != value)
                {
                    _bool = value;
                    OnValueChanged?.Invoke(_bool);
                }
            }
        }
    }
}