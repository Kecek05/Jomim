using UnityEngine;

namespace KeceK.Utils
{
    /// <summary>
    /// This ScriptableObject holds references to sprites relative to each player type.
    /// </summary>
    [CreateAssetMenu(fileName = "SpriteIdentifierData", menuName = "Scriptable Objects/Identifiers/SpriteIdentifierData")]
    public class SpriteIdentifierDataSO : ScriptableObject
    {
        public Sprite player1Sprite;
        public Sprite player2Sprite;
    }
}
