using System;
using System.Collections;
using UnityEngine.Localization.Settings;

namespace KeceK.General
{
    public static class LocalizationManager
    {
        public static IEnumerator InitializeLocalizationBasedOnSave(Action onInitialized = null)
        {
            yield return LocalizationSettings.InitializationOperation;
            
            int savedLocalizationIndex = Saver.GetSavedLocalizationIndex();
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[savedLocalizationIndex];
            onInitialized?.Invoke();
        }
        
        public static IEnumerator SetLocalizationByIndex(int localeIndex, Action onChanged = null)
        {
            yield return LocalizationSettings.InitializationOperation;
            
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeIndex];
            Saver.SaveLocalizationByIndex(localeIndex);
            onChanged?.Invoke();
        }
    }
}
