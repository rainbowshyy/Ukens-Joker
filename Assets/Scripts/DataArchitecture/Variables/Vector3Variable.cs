using UnityEngine;

namespace UkensJoker.DataArchitecture
{
    [CreateAssetMenu(fileName = "Vector3Var", menuName = "Variables/Vector 3")]
    public class Vector3Variable : ScriptableObject
    {
        public Vector3 Value;
    }
}