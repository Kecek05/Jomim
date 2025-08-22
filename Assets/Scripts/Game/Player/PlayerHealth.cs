using System;
using KeceK.General;
using Sirenix.OdinInspector;
using UnityEngine;

namespace KeceK.Game
{
    public class PlayerHealth : MonoBehaviour, IDamageable
    {
        public event Action OnDeath;
        
        [SerializeField] [Title("Settings")]
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
            if(_isDead) return;
            
            _currentHealth -= damage;
            Debug.Log(_currentHealth);
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            _isDead = true;
            OnDeath?.Invoke();
        }
    }
}
