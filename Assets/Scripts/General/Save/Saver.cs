using UnityEngine;

namespace KeceK.General
{
    public static class Saver
    {
        private static string Save_UnlockedLevel_Key = "UnlockedLevel_Key";
        
        public static void SaveUnlockedLevelByIndex(int levelIndex)
        {
            PlayerPrefs.SetInt(Save_UnlockedLevel_Key, levelIndex);
            PlayerPrefs.Save();
        }
        
        public static int GetSavedUnlockedLevelEnumIndex()
        {
            return PlayerPrefs.GetInt(Save_UnlockedLevel_Key, 0);
        }
        
        public static void DeleteSavedUnlockedLevels()
        {
            PlayerPrefs.DeleteKey(Save_UnlockedLevel_Key);
        }
    }
}
