using System;
using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Utils.Components
{
    public class DamageOnContact : MonoBehaviour
    {
        [SerializeField] [FoldoutGroup("Settings")]
        private float _damageOnContact;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.rigidbody.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damageOnContact);
            }
        }
    }
}
