using UnityEngine;

namespace KeceK.General
{
    public static class Saver
    {
        private static string Save_Level_Key = "Level_Key";
        
        public static void SaveLevelByIndex(int levelIndex)
        {
            if(levelIndex <= GetSavedLevelEnumIndex()) return;
            
            PlayerPrefs.SetInt(Save_Level_Key, levelIndex);
            PlayerPrefs.Save();
        }
        
        public static int GetSavedLevelEnumIndex()
        {
            return PlayerPrefs.GetInt(Save_Level_Key, 0);
        }
        
        public static void DeleteSavedLevels()
        {
            PlayerPrefs.DeleteKey(Save_Level_Key);
        }
    }
}
