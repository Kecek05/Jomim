using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.UI
{
    [CreateAssetMenu(fileName = "LevelPasswordSO", menuName = "Scriptable Objects/LevelPasswordSO")]
    public class LevelPasswordSO : ScriptableObject
    {
        [MultiLineProperty(10)]
        public string Hint;
        public string Password;
        [Space(10f)]
        [Tooltip("Should Consider the Locker Levels in the index count")]
        public int LevelToUnlockIndex;
        
    }
}
