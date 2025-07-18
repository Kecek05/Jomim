using KeceK.General;
using UnityEngine;

namespace KeceK.Utils.Components
{
    public class CollectOnTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ICollectable collectable))
            {
                collectable.Collect();
            }
        }
    }
}
