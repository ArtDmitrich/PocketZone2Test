using System;
using UnityEngine;

namespace GameLogic.Characters
{
    public class HealthComponent : MonoBehaviour
    {
        public event Action CharacterDied;
        public event Action<float, float> HealthChanged;

        private IHealthStats _characterStats;
        private float _currentHealth;

        public void Init(IHealthStats characterStats)
        {
            _characterStats = characterStats;
            _currentHealth = _characterStats.MaxHealth;
            HealthChanged?.Invoke(_currentHealth, _characterStats.MaxHealth);
        }

        public void GetDamage(float value)
        {
            ChangeCurrentHealth(-value);
        }

        public void Heal(float value)
        {
            ChangeCurrentHealth(value);
        }

        private void ChangeCurrentHealth(float value)
        {
            _currentHealth += value;

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                CharacterDied?.Invoke();
            }
            else if (_currentHealth > _characterStats.MaxHealth)
            {
                _currentHealth = _characterStats.MaxHealth;
            }
            
            HealthChanged?.Invoke(_currentHealth, _characterStats.MaxHealth);
        }
    }
}
