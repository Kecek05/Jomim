using UnityEngine;

namespace KeceK.Utils.ScriptableObjects
{
    /// <summary>
    /// This ScriptableObject holds references to GameObjects Prefabs relative to each player type.
    /// </summary>
    [CreateAssetMenu(fileName = "GameObjectIdentifierDataSO", menuName = "Scriptable Objects/Identifiers/GameObjectIdentifierData")]
    public class GameObjectIdentifierDataSO : ScriptableObject
    {
        public GameObject player1GameObject;
        public GameObject player2GameObject;
    }
}
