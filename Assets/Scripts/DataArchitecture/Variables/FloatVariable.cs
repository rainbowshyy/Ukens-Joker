using System;
using UnityEngine;

namespace UkensJoker.DataArchitecture
{
    [CreateAssetMenu(fileName = "FloatVar", menuName = "Variables/Float")]
    public class FloatVariable : ScriptableObject
    {
        [SerializeField] private float _float;

        public event Action<float> OnValueChanged;

        public float Value
        {
            get { return _float; }
            set
            {
                if (_float != value)
                {
                    _float = value;
                    OnValueChanged?.Invoke(value);
                }
            }
        }
    }
}