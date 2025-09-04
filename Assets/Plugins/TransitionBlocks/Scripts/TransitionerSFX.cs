using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.TransitionBlocks.Scripts
{
    public class TransitionerSFX : MonoBehaviour
    {
        [Title("References")]
        [SerializeField] [Required] private AudioSource _fadeInSFX;
        [SerializeField] [Required] private AudioSource _fadeOutSFX;

        [Title("Settings")]
        [SerializeField] private float _fadeInDelay = 0.1f;
        [SerializeField] private float _fadeOutDelay = 0.1f;
        
        private void Start()
        {
            Transitioner.OnTransitioningOutScene += TransitionerOnOnTransitioningOutScene;
            Transitioner.OnTransitioningInAScene += TransitionerOnOnTransitioningInAScene;
        }
        
        private void OnDestroy()
        {
            Transitioner.OnTransitioningOutScene -= TransitionerOnOnTransitioningOutScene;
            Transitioner.OnTransitioningInAScene -= TransitionerOnOnTransitioningInAScene;
        }
        
        [Button("Fade Out SFX")]
        private void TransitionerOnOnTransitioningOutScene()
        {
            _fadeOutSFX.PlayDelayed(_fadeOutDelay);
        }

        [Button("Fade In SFX")]
        private void TransitionerOnOnTransitioningInAScene()
        {
            _fadeInSFX.PlayDelayed(_fadeInDelay);
        }
    }
}
