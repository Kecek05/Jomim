using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Game
{
    public class DoorSFX : MonoBehaviour
    {
        [Title("References")]
        [SerializeField]  [Required]
        private Door _door;
        
        [Space(10f)]
        [SerializeField] [Required]
        private AudioSource _openSFX;
        [SerializeField] [Required]
        private AudioSource _closeSFX;
        
        private void OnEnable()
        {
            _door.OnActivatableStateChanged += DoorOnOnActivatableStateChanged;
        }

        private void OnDisable()
        {
            _door.OnActivatableStateChanged -= DoorOnOnActivatableStateChanged;
        }
        
        [Button]
        private void DoorOnOnActivatableStateChanged(bool active)
        {
            if (active)
                _openSFX?.Play();
            else
                _closeSFX?.Play();
        }
    }
}
