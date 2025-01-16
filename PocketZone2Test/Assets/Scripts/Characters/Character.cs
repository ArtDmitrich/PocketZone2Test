using System;
using UnityEngine;

namespace Characters
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

        public virtual void TakeDamage(float damage)
        {
            Health.GetDamage(damage);
            Model.PlayShortAnimation(ModelAnimation.Hurt);
        }

        private void Death()
        {
            CharacterDead?.Invoke(this);
            Model.PlayShortAnimation(ModelAnimation.Death);
        }

        protected virtual void OnEnable()
        {
            Health.CharacterDied += Death;
            Health.Init(Stats);
        }

        protected virtual void OnDisable()
        {
            Health.CharacterDied -= Death;
        }
    }
}
