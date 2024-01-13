using UnityEngine;

namespace UkensJoker.DataArchitecture
{
    [CreateAssetMenu(fileName = "IntVar", menuName = "Variables/Int")]
    public class IntVariable : ScriptableObject
    {
        public int Value;
    }
}