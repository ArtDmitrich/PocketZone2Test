using UnityEngine;
using UnityEngine.Serialization;

namespace GameLogic.Characters
{
    public class CharacterStats : MonoBehaviour, IHealthStats, IMovementStats
    {
        public float MaxHealth => _maxHealth;
        public float MovementSpeed => _movementSpeed;
        public float MeleeDamageValue => _meleeDamageValue;
        public float MeleeAttackCooldown => _meleeAttackCooldown;
        public float MeleeAttackDistance => _meleeAttackDistance;
        
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _meleeDamageValue;
        [SerializeField] private float _meleeAttackCooldown;
        [SerializeField] private float _meleeAttackDistance;
    }
}
