using System;
using System.Collections;
using KeceK.General;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KeceK.UI
{
    public class LevelLockedCanvas : MonoBehaviour
    {
        public event Action<bool> OnPasswordSubmitted;
        
        [Title("References")]
        [SerializeField] private TextMeshProUGUI _hintText;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _submitButton;
        [Space(15f)]
        [SerializeField] private LevelPasswordSO _levelPasswordSO;

        private void Start()
        {
            _hintText.text = _levelPasswordSO.Hint;
        }

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
            Saver.SaveUnlockedLevelByIndex(_levelPasswordSO.LevelToUnlockIndex);
            OnPasswordSubmitted?.Invoke(true);
            StartCoroutine(DelayToChangeScene());
        }
        
        private IEnumerator DelayToChangeScene()
        {
            yield return new WaitForSeconds(1f);
            Loader.LoadNextLevel();
        }

        private void IncorrectPassword()
        {
            OnPasswordSubmitted?.Invoke(false);
        }
    }
}
