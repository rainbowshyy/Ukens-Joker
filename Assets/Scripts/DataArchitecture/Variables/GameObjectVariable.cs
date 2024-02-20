using UnityEngine;

namespace UkensJoker.DataArchitecture
{
    [CreateAssetMenu(fileName = "GameObjectVar", menuName = "Variables/GameObjectReference")]
    public class GameObjectVariable : ScriptableObject
    {
        public GameObject Value;
    }
}