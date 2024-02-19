using System;

namespace UkensJoker.DataArchitecture
{
    [Serializable]
    public class BoolReference
    {
        public bool UseConstant = true;
        public bool ConstantValue;
        public BoolVariable Variable;

        public bool Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }

        public void RegisterListener(Action<bool> onValueChanged)
        {
            Variable.OnValueChanged += onValueChanged;
        }

        public void UnregisterListener(Action<bool> onValueChanged)
        {
            Variable.OnValueChanged -= onValueChanged;
        }
    }
}