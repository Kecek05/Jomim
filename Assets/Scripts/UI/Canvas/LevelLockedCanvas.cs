using System;
using KeceK.General;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KeceK.UI
{
    public class LevelLockedCanvas : MonoBehaviour
    {
        public static event Action OnIncorrectPassword;
        
        [Title("References")]
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _submitButton;
        [SerializeField] private LevelPasswordSO _levelPasswordSO;
        
        [Title("Settings")]
        [SerializeField] private int _levelToUnlockIndex;

        private void OnEnable()
        {
            _submitButton.onClick.AddListener(() =>
            {
                SubmitPassword();
            });
        }

        private void SubmitPassword()
        {
            if(_inputField.text == _levelPasswordSO.Password)
            {
                CorrectPassword();
            }
            else
            {
                IncorrectPassword();
            }
        }
        
        private void CorrectPassword()
        {
            Debug.Log("Correct Password");
            _submitButton.interactable = false;
            _inputField.interactable = false;
            Saver.SaveUnlockedLevelByIndex(_levelToUnlockIndex);
            Loader.LoadNextLevel();
        }

        private void IncorrectPassword()
        {
            Debug.Log("Wrong Password");
            OnIncorrectPassword?.Invoke();
        }
    }
}
