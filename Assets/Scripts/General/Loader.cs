using System;
using System.Text.RegularExpressions;
using Plugins.TransitionBlocks.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KeceK.General
{
    public static class Loader
    {
        private readonly static string LockerSufix = "Locker";
        
        public static event Action<Scene> OnCurrentSceneChanged;
        
        private static Scene _currentScene;
        
        public static Scene CurrentScene => _currentScene;
    
        public enum Scene
        {
            Level1,
            Level2,
            Level3,
            Level4,
            Level5,
            Level6,
            Level7,
            Level8,
            Level9,
            Level10,
            None,
            MainMenu,
            Loading,
        }
        
        /// <summary>
        /// Load the scene and invoke OnCurrentSceneChanged
        /// </summary>
        /// <param name="scene"></param>
        public static void Load(Scene scene)
        {
            _currentScene = scene;
            if (Transitioner.Instance != null)
            {
                Transitioner.Instance.TransitionToScene(_currentScene.ToString(), true);
            }
            else
            {
                SceneManager.LoadScene(_currentScene.ToString());
            }
            OnCurrentSceneChanged?.Invoke(_currentScene);
        }
        
        public static void ReloadCurrentScene()
        {
            Load(_currentScene);
        }

        public static void LoadNextLevel()
        {
            int nextSceneIndex = (int)_currentScene + 1;

            if (nextSceneIndex < Enum.GetNames(typeof(Scene)).Length)
            {
                Scene nextScene = (Scene)nextSceneIndex;

                if (IsLevel(nextScene))
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

        public static void SetCurrentSceneDebugOnly(Scene scene)
        {
            _currentScene = scene;
        }
    }
}
