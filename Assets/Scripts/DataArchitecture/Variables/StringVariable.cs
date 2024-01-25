using System;
using UnityEngine;

namespace UkensJoker.DataArchitecture
{
    [CreateAssetMenu(fileName = "StringVar", menuName = "Variables/String")]
    public class StringVariable : ScriptableObject
    {
        [SerializeField] private string _string;

        public event Action<string> OnValueChanged;

        public string Value { 
            get { return _string; } 
            set
            {
                if (_string != value)
                {
                    _string = value;
                    OnValueChanged?.Invoke(_string);
                }
            }
        }
    }
}
