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
        
        private void TransitionerOnOnTransitioningOutScene()
        {
            _fadeOutSFX.Play();
        }

        private void TransitionerOnOnTransitioningInAScene()
        {
            _fadeInSFX.Play();
        }
    }
}
