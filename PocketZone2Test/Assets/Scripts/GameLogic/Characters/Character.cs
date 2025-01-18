using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace GameLogic.Characters
{
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(CharacterStats))]
    public class Character : MonoBehaviour, ITakingDamage
    {
        public event Action<Character> CharacterDead;
        
        protected CharacterStats Stats { get { return _stats ??= GetComponent<CharacterStats>(); } }
        private CharacterStats _stats;
        private HealthComponent Health { get { return _health ??= GetComponent<HealthComponent>(); } }
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

        protected virtual void Initialization()
        {
            Health.Init(Stats);
            Model.PlayShortAnimation(ModelAnimation.Resurrection);
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
        
        private void Death()
        {
            CharacterDead?.Invoke(this);
            Model.PlayShortAnimation(ModelAnimation.Death);
        }
    }
}
