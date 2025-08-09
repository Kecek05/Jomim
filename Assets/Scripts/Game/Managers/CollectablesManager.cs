using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Game
{
    public class CollectablesManager : MonoBehaviour
    {
        public static event Action OnCoinCollectedP1;
        public static event Action OnCoinCollectedP2;
        public static event Action OnCollectedAllCoins;
        
        [Title("Settings")]
        [SerializeField] 
        private int _collectablesToWinP1 = 5;
        [SerializeField]
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
                    OnCoinCollectedP1?.Invoke();
                    break;
                case 2:
                    _collectedCoinsP2++;
                    OnCoinCollectedP2?.Invoke();
                    break;
            }

            CheckCollectedAllCoins();
        }


        private void CheckCollectedAllCoins()
        {
            if(_collectedCoinsP1 >= _collectablesToWinP1 && _collectedCoinsP2 >= _collectablesToWinP2)
                OnCollectedAllCoins?.Invoke();
        }
        
        [Button] [HideInEditorMode]
        private void ForceCollectAllCoins()
        {
            _collectedCoinsP1 = _collectablesToWinP1;
            _collectedCoinsP2 = _collectablesToWinP2;
            OnCollectedAllCoins?.Invoke();
        }
    }
}
