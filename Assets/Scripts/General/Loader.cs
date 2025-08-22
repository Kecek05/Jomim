using System;
using Plugins.TransitionBlocks.Scripts;

namespace KeceK.General
{
    public static class Loader
    {
        public static event Action<Scene> OnCurrentSceneChanged;
        
        private static Scene currentScene;
        
        public static Scene CurrentScene => currentScene;
    
        public enum Scene
        {
            MainMenu,
            Loading,
            Level1,
            Level2,
            Level3,
            Level4,
            Level5,
            Level6,
            Level7,
            Level8,
        }
        
        public static void Load(Scene scene)
        {
            currentScene = scene;
            
            Transitioner.Instance.TransitionToScene(currentScene.ToString());
            OnCurrentSceneChanged?.Invoke(currentScene);
        }
    }
}
