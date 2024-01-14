using System;
using UnityEngine;

namespace UkensJoker.DataArchitecture
{
    [CreateAssetMenu(fileName = "StringVar", menuName = "Variables/String")]
    public class StringVariable : ScriptableObject
    {
        [SerializeField] private string _string;

        public Action<string> OnValueChanged { get; private set; }

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

        public void RegisterListener(Action<string> onValueChanged)
        {
            OnValueChanged += onValueChanged;
        }

        public void UnregisterListener(Action<string> onValueChanged)
        {
            OnValueChanged -= onValueChanged;
        }
    }
}
