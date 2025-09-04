using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Game
{
    public class CoinSFX : MonoBehaviour
    {
        [Title("References")]
        [SerializeField] [Required]
        private Coin _coin;
        
        [SerializeField] [Required] private AudioSource _collectAudioSource;

        private void OnEnable()
        {
            _coin.OnThisCoinCollected += CoinOnOnThisCoinCollected;
        }

        private void OnDisable()
        {
            _coin.OnThisCoinCollected -= CoinOnOnThisCoinCollected;
        }

        [Button("Coin Collected SFX")]
        private void CoinOnOnThisCoinCollected()
        {
            _collectAudioSource.Play();
        }
    }
}
