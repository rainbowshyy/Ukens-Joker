using System;
using UnityEngine;

namespace UkensJoker.DataArchitecture
{
    [CreateAssetMenu(fileName = "IntVar", menuName = "Variables/Int")]
    public class IntVariable : ScriptableObject
    {
        [SerializeField] private int _int;

        public event Action<int> OnValueChanged;

        public int Value
        {
            get { return _int; }
            set
            {
                if (_int != value)
                {
                    _int = value;
                    OnValueChanged?.Invoke(_int);
                }
            }
        }
    }
}