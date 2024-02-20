using System;
using UnityEngine;

namespace UkensJoker.DataArchitecture
{
    [CreateAssetMenu(fileName = "Vector2Var", menuName = "Variables/Vector 2")]
    public class Vector2Variable : ScriptableObject
    {
        [SerializeField] private Vector2 _vector;

        public event Action<Vector2> OnValueChanged;

        public Vector2 Value
        {
            get { return _vector; }
            set
            {
                if (_vector != value)
                {
                    _vector = value;
                    OnValueChanged?.Invoke(value);
                }
            }
        }
    }
}