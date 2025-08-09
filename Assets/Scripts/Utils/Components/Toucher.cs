using System;
using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Utils.Components
{
    public class Toucher : MonoBehaviour
    {
        public event Action<Collider2D> OnTriggerEnterEvent;
        
        [SerializeField] [Title("Settings")]
        private bool _onTrigger = true;
        [SerializeField]
        private bool _onCollision = true;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(!_onCollision) return;
            // Debug.Log($"Collision Enter: {other.gameObject.name} with {other.otherCollider.gameObject.name}");
            if (other.gameObject.TryGetComponent(out ITouchable touchable))
            {
                touchable.Touch(other.otherCollider.gameObject);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if(!_onCollision) return;
            // Debug.Log($"Collision Exit: {other.gameObject.name} with {other.otherCollider.gameObject.name}");
            if (other.gameObject.TryGetComponent(out IUnTouchable touchable))
            {
                touchable.Untouch(other.otherCollider.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_onTrigger) return;
            // Debug.Log($"Trigger Enter: {other.gameObject.name} with {gameObject.name}");
            if (other.gameObject.TryGetComponent(out ITouchable touchable))
            {
                touchable.Touch(gameObject);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!_onTrigger) return;
            // Debug.Log($"Trigger Exit: {other.gameObject.name} with {gameObject.name}");
            if (other.gameObject.TryGetComponent(out IUnTouchable touchable))
            {
                touchable.Untouch(gameObject);
            }
        }
    }
}
