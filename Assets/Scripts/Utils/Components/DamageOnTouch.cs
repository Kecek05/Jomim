using System;
using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Utils.Components
{
    public class DamageOnTouch : MonoBehaviour, ITouchable
    {
        [SerializeField] [FoldoutGroup("Settings")]
        private float _damageOnContact;

        public void Touch(GameObject whoTouchedMe)
        {
            if (whoTouchedMe.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damageOnContact);
            }
        }
    }
}
