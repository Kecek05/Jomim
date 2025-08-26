using UnityEngine;

namespace KeceK.Game
{
    [CreateAssetMenu(fileName = "GameSettingsSO", menuName = "Scriptable Objects/GameSettingsSO")]
    public class GameSettingsSO : ScriptableObject
    {
        public float DelayToChangeLevel = 1.5f;
        public float DelayToReloadLevel = 1.5f;
    }
}
