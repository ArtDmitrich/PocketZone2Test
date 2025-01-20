using Cysharp.Threading.Tasks;
using Services.ObjectPool.ZenjectVariant;
using Services.StateMachine;
using UnityEngine;
using Weapons;
using Zenject;

namespace GameLogic.Characters
{
    public class MeleeEnemyCharacter : MovableCharacter
    {
        public class Factory : PlaceholderFactory<MeleeEnemyCharacter>
        {
        
        }
        
        public class Pool : MemoryPool<MeleeEnemyCharacter>
        {
            protected override void OnCreated(MeleeEnemyCharacter enemy)
            {
                enemy.gameObject.SetActive(false);
            }

            protected override void OnSpawned(MeleeEnemyCharacter enemy)
            {
                enemy.gameObject.SetActive(true);
            }

            protected override void OnDespawned(MeleeEnemyCharacter enemy)
            {
                enemy.gameObject.SetActive(false);
            }
        }
        
        private TargetRadar Radar { get { return _radar ??= GetComponentInChildren<TargetRadar>(); } }
        private TargetRadar _radar;
        
        private MeleeEnemyStateMachine _stateMachine;
        private Transform _target;
        private bool _isCanAttack;
        
        private Pool _pool;

        [Inject]
        private void Construct(Pool pool)
        {
            _pool = pool;
        }
        
        public override void Initialize()
        {
            base.Initialize();
            
            _stateMachine = new MeleeEnemyStateMachine(this);
            ChangeState(new IdleState(this));
            _isCanAttack = true;
        }

        public void StartMoveToTarget()
        {
            var direction = _target.position - transform.position;
            
            StartMovement(direction.normalized);
        }
        
        public void StopMove()
        {
            StopMovement();
        }

        public bool CheckNearestTarget()
        {
            _target = Radar.GetNearestTarget();
            
            return _target != null;
        }

        public bool CheckAttackDistance()
        {
            var distance = Vector2.Distance(transform.position, _target.position);
            
            return distance <= Stats.MeleeAttackDistance;
        }

        public bool CheckAttackPosibility()
        {
            return _isCanAttack && _target != null && CheckAttackDistance();
        }

        public void StartAttack()
        {
            Cooldown();
            Model.PlayShortAnimation(ModelAnimation.Attack);
        }

        public void ChangeState(State<MeleeEnemyCharacter> newState)
        {
            _stateMachine.ChangeState(newState);
        }

        protected override void Death()
        {
            base.Death();
            
            ChangeState(new DeathState(this));
            _pool.Despawn(this);
        }

        private void EndAttack()
        {
            if (_target != null && CheckAttackDistance() 
                                && _target.TryGetComponent<ITakingDamage>(out ITakingDamage takingDamage))
            {
                takingDamage.TakeDamage(Stats.MeleeDamageValue);
            }
        }
        
        private async UniTask Cooldown()
        {
            _isCanAttack = false;
            
            await UniTask.Delay((int)(Stats.MeleeAttackCooldown * 1000));
            
            _isCanAttack = true;
        }

        private void Update()
        {
            _stateMachine.UpdateState();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            Model.MeleeAttackEnded += EndAttack;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            Model.MeleeAttackEnded -= EndAttack;
        }
    }
}
