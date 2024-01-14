using System;

namespace UkensJoker.DataArchitecture
{
    [Serializable]
    public class StringReference
    {
        public bool UseConstant = true;
        public string ConstantValue;
        public StringVariable Variable;

        public string Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }

        public void RegisterListener(Action<string> onValueChanged)
        {
            Variable.RegisterListener(onValueChanged);
        }

        public void UnregisterListener(Action<string> onValueChanged)
        {
            Variable.UnregisterListener(onValueChanged);
        }
    }
}
