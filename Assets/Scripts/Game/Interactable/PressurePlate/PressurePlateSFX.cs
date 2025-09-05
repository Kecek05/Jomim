using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Game
{
    public class PressurePlateSFX : MonoBehaviour
    {
        [Title("References")]
        [SerializeField] [Required]
        private PressurePlate _pressurePlate;
        
        [Space(10f)]
        [SerializeField] [Required]
        private AudioSource _activateSFX;
        [SerializeField] [Required]
        private AudioSource _deactivateSFX;
        
        private void OnEnable()
        {
            _pressurePlate.OnActivationStateChanged += PressurePlateOnOnActivationStateChanged;
        }

        private void OnDisable()
        {
            _pressurePlate.OnActivationStateChanged -= PressurePlateOnOnActivationStateChanged;
        }
        
        [Button]
        private void PressurePlateOnOnActivationStateChanged(bool activated)
        {
            if (activated)
                _activateSFX?.Play();
            else
                _deactivateSFX?.Play();
        }
    }
}
