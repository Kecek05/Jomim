using KeceK.General;
using UnityEngine;

namespace KeceK.Game
{
    public class Coin : MonoBehaviour, ICollectable
    {
        [SerializeField] private GameObject _particleSystemPrefab;
        public void Collect()
        {
            Debug.Log("Collected Coin");
            Instantiate(_particleSystemPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
