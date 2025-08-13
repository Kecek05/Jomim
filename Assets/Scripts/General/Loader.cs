using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KeceK.General
{
    public static class Loader
    {
        public static event Action<Scene> OnCurrentSceneChanged;
    
        private static Scene targetScene;
        private static Scene currentScene;
        
        public static Scene CurrentScene => currentScene;
    
        public enum Scene
        {
            MainMenu,
            Loading,
            level1,
            level2,
            level3,
            level4,
            level5,
            level6,
            level7,
            level8,
        }
    
        /// <summary>
        /// Load a scene with a loading screen.
        /// </summary>
        /// <param name="scene"></param>
        public static void Load(Scene scene)
        {
            targetScene = scene;
            currentScene = Scene.Loading;
            
            SceneManager.LoadScene(Scene.Loading.ToString());
            OnCurrentSceneChanged?.Invoke(currentScene);
        }
        
        public static void LoadNoLoadingScreen(Scene scene)
        {
            currentScene = scene;
    
            SceneManager.LoadScene(scene.ToString());
            OnCurrentSceneChanged?.Invoke(currentScene);
        }
    
        public static void LoadCallback()
        {
            SceneManager.LoadScene(targetScene.ToString());
            currentScene = targetScene;
            OnCurrentSceneChanged?.Invoke(currentScene);
        }
    }
}
