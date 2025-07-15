using System;
using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Game
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] [FoldoutGroup("Settings")]
        private float _maxHealth = 100f;
        
        
        private float _currentHealth;
        private bool _isDead;
        
        //Publics
        public bool IsDead => _isDead;

        private void OnEnable()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float damage)
        {
            if(IsDead) return;
            
            _currentHealth -= damage;
            Debug.Log(_currentHealth);
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            //Temporary death handling
            _isDead = true;
            Destroy(gameObject);
        }
    }
}
