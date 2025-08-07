using UnityEngine;

namespace KeceK.Utils
{
    [CreateAssetMenu(fileName = "SpriteIdentifierData", menuName = "Scriptable Objects/SpriteIdentifierData")]
    public class SpriteIdentifierDataSO : ScriptableObject
    {
        public Sprite player1Sprite;
        public Sprite player2Sprite;
    }
}
