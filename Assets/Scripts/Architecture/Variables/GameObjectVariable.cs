using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectVar", menuName = "Variables/GameObjectReference")]
public class GameObjectVariable : ScriptableObject
{
    public GameObject Value;
}