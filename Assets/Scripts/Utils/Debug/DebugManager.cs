using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KeceK.Utils.Debug
{
    public class DebugManager : MonoBehaviour
    {
        #if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                // Parse string to enum
                string sceneName = SceneManager.GetActiveScene().name; // Example scene name
                if (Enum.TryParse(sceneName, out General.Loader.Scene scene))
                {
                    General.Loader.Load(scene);
                }
                else
                {
                    UnityEngine.Debug.LogError($"Failed to parse scene name: {sceneName}");
                }
            }
        }
        #endif 
    }
}
