using UnityEngine;

namespace Characters
{
    public class CharacterStats : MonoBehaviour, IHealthStats, IMovementStats
    {
        public float MaxHealth => _maxHealth;
        public float MovementSpeed => _movementSpeed;
        
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _movementSpeed;
    }
}
