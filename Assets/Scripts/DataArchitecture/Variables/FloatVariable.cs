using UnityEngine;

namespace UkensJoker.DataArchitecture
{
    [CreateAssetMenu(fileName = "FloatVar", menuName = "Variables/Float")]
    public class FloatVariable : ScriptableObject
    {
        public float Value;
    }
}