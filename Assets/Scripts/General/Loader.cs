using System;
using Plugins.TransitionBlocks.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KeceK.General
{
    public static class Loader
    {
        public static event Action<Scene> OnCurrentSceneChanged;
        
        private static Scene currentScene;
        
        public static Scene CurrentScene => currentScene;
    
        public enum Scene
        {
            None,
            MainMenu,
            Loading,
            Level1,
            Level2Locker,
            Level2,
            Level3Locker,
            Level3,
            Level4Locker,
            Level4,
            Level5Locker,
            Level5,
            Level6Locker,
            Level6,
            Level7Locker,
            Level7,
            Level8Locker,
            Level8,
            Level9Locker,
            Level9,
            Level10Locker,
            Level10,
        }
        
        /// <summary>
        /// Load the scene and invoke OnCurrentSceneChanged
        /// </summary>
        /// <param name="scene"></param>
        public static void Load(Scene scene)
        {
            currentScene = scene;
            if (Transitioner.Instance != null)
            {
                Transitioner.Instance.TransitionToScene(currentScene.ToString());
            }
            else
            {
                SceneManager.LoadScene(currentScene.ToString());
            }
            OnCurrentSceneChanged?.Invoke(currentScene);
        }
        
        public static void ReloadCurrentScene()
        {
            Load(currentScene);
        }

        public static void LoadNextLevel()
        {
            int nextSceneIndex = (int)currentScene + 1;

            if (nextSceneIndex < Enum.GetNames(typeof(Scene)).Length)
            {
                Scene nextScene = (Scene)nextSceneIndex;
                
                if(IsLevel(nextScene))
                    Load(nextScene);
                else
                {
                    Debug.Log($"Next scene is not a level. The Scene is: {nextScene}. Going to menu");
                    Load(Scene.MainMenu);
                }
            }
        }

        private static bool IsLevel(Scene scene)
        {
            string sceneName = scene.ToString();
            
            if (string.IsNullOrEmpty(sceneName)) return false;
            
            return sceneName.IndexOf("Level", StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
