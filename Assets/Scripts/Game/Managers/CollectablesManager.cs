using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Game
{
    public class CollectablesManager : MonoBehaviour
    {
        public static event Action OnCoinCollectedP1;
        public static event Action OnCoinCollectedP2;
        
        public static event Action OnCoinCollectedAllP1;
        public static event Action OnCoinCollectedAllP2;
        
        [SerializeField] [FoldoutGroup("Settings")]
        private int _collectablesToWinP1 = 5;
        [SerializeField] [FoldoutGroup("Settings")]
        private int _collectablesToWinP2 = 5;

        private int _collectedCoinsP1, _collectedCoinsP2;
        
        private void Start()
        {
            Coin.OnCoinCollected += CoinOnOnCoinCollected;
        }

        private void OnDestroy()
        {
            Coin.OnCoinCollected -= CoinOnOnCoinCollected;
        }

        private void CoinOnOnCoinCollected(int playerCollected)
        {
            switch (playerCollected)
            {
                case 1:
                    _collectedCoinsP1++;
                    break;
                case 2:
                    _collectedCoinsP2++;
                    break;
            }

            CheckCollectedAllCoins();
        }

        private void CheckCollectedAllCoins()
        {
            //Check if collected all coins to trigger events and unlock the exit
        }
    }
}
