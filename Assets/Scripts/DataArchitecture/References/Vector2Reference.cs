using System;
using UnityEngine;

namespace UkensJoker.DataArchitecture
{
    [Serializable]
    public class Vector2Reference
    {
        public bool UseConstant = true;
        public Vector2 ConstantValue;
        public Vector2Variable Variable;

        public Vector2 Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }

        public void RegisterListener(Action<Vector2> onValueChanged)
        {
            Variable.OnValueChanged += onValueChanged;
        }

        public void UnregisterListener(Action<Vector2> onValueChanged)
        {
            Variable.OnValueChanged -= onValueChanged;
        }
    }
}