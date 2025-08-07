using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Game
{
    public class PressurePlateGFX : MonoBehaviour
    {
        private readonly string touchingAnimator = "Touching";
        
        [SerializeField] [Required] [FoldoutGroup("References")]
        private Animator _animator;
        [SerializeField] [Required] [FoldoutGroup("References")]
        private PressurePlate _pressurePlate;
        
        private void OnEnable()
        {
            _pressurePlate.OnActivationStateChanged += PressurePlateOnOnActivationStateChanged;
        }

        private void OnDisable()
        {
            _pressurePlate.OnActivationStateChanged -= PressurePlateOnOnActivationStateChanged;
        }

        private void PressurePlateOnOnActivationStateChanged(bool isTouching)
        {
            _animator.SetBool(touchingAnimator, isTouching);
        }
    }
}
