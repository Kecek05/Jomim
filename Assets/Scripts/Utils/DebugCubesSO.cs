using KeceK.Plugins.EditableAssetAttribute;
using UnityEngine;

namespace KeceK.Utils
{
    [CreateAssetMenu(fileName = "DebugCubesSO", menuName = "Scriptable Objects/DebugCubesSO")]
    public class DebugCubesSO : ScriptableObject
    {
        public GameObject[] DebugCubePrefabs;
    }
}
