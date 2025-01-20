using System;
using UnityEngine;

namespace GameLogic.Characters
{
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(CharacterStats))]
    public class Character : MonoBehaviour, ITakingDamage
    {
        public event Action<Character> CharacterDead;
        
        [SerializeField] private Collider2D _characterCollider;
        
        protected CharacterStats Stats { get { return _stats ??= GetComponent<CharacterStats>(); } }
        private CharacterStats _stats;
        protected HealthComponent Health { get { return _health ??= GetComponent<HealthComponent>(); } }
        private HealthComponent _health;
        
        protected CharacterModel Model { get { return _model ??= GetComponentInChildren<CharacterModel>(); } }
        private CharacterModel _model;
        
        private Healthbar Healthbar { get { return _healthbar ??= GetComponentInChildren<Healthbar>(); } }
        private Healthbar _healthbar;

        public virtual void TakeDamage(float damage)
        {
            Health.GetDamage(damage);
            Model.PlayShortAnimation(ModelAnimation.Hurt);
        }
        
        public virtual void Initialize()
        {
            _characterCollider.enabled = true;
            Health.Init(Stats);
            Model.PlayShortAnimation(ModelAnimation.Resurrection);
        }
        
        protected virtual void Death()
        {
            _characterCollider.enabled = false;
            CharacterDead?.Invoke(this);
            Model.PlayShortAnimation(ModelAnimation.Death);
        }

        protected virtual void OnEnable()
        {
            Health.CharacterDied += Death;
            Health.HealthChanged += Healthbar.UpdateHealthBar;
        }

        protected virtual void OnDisable()
        {
            Health.CharacterDied -= Death;
            Health.HealthChanged += Healthbar.UpdateHealthBar;
        }
    }
}
