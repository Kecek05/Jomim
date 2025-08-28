using System;
using UnityEngine;

namespace KeceK.UI
{
    public class PasswordFeedback : MonoBehaviour
    {
        private readonly string _wrongTrigger = "Wrong";
        
        [SerializeField] private LevelLockedCanvas _levelLockedCanvas;
        [SerializeField] private Animator _feedbackTextAnimator;
        [SerializeField] private AudioSource _wrongSound;
        [SerializeField] private AudioSource _correctSound;
        
        private void OnEnable()
        {
            _levelLockedCanvas.OnPasswordSubmitted += LevelLockedCanvasOnOnPasswordSubmitted;
        }

        private void OnDisable()
        {
            _levelLockedCanvas.OnPasswordSubmitted -= LevelLockedCanvasOnOnPasswordSubmitted;
        }
        
        private void LevelLockedCanvasOnOnPasswordSubmitted(bool isCorrect)
        {
            if (isCorrect)
            {
                _correctSound.Play();
            }
            else
            {
                _wrongSound.Play();
                _feedbackTextAnimator.SetTrigger(_wrongTrigger);
            }
        }

        private void LevelLockedCanvasOnOnPasswordSubmitted()
        {

        }
    }
}
