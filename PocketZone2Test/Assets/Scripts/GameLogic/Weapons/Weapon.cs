using Cysharp.Threading.Tasks;
using GameLogic.Bullet;
using UnityEngine;
using Weapons;

namespace GameLogic.Weapons
{
    [RequireComponent(typeof(WeaponStats))]
    [RequireComponent(typeof(TargetRadar))]
    public class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private string _bulletName;
        [SerializeField] private Transform _bulletSlot;
        private WeaponModel Model { get { return _model ??= GetComponentInChildren<WeaponModel>(); } }
        private WeaponModel _model;
        
        private WeaponStats Stats { get { return _stats ??= GetComponent<WeaponStats>(); } }
        private WeaponStats _stats;

        private TargetRadar Radar  { get {return _radar ??= GetComponent<TargetRadar>(); } }
        private TargetRadar _radar;

        private bool _isCanAttack = true;

        public void Attack()
        {
            if (!_isCanAttack)
            {
                return;
            }

            var target = Radar.GetNearestTarget();

            if (target == null)
            {
                return;
            }

            var bullet = BulletPoolManager.Instance.GetBullet(_bulletName);

            if (bullet == null)
            {
                return;
            }
            
            bullet.Initialization(Random.Range(Stats.MinDamage, Stats.MaxDamage), Radar.TargetType);
            bullet.transform.position = _bulletSlot.position;
            var vectorToTarget = target.position - _bulletSlot.position;
            bullet.StartMovement(vectorToTarget.normalized);

            Model.PlayAttackAnimation();
            Cooldown();
        }

        private async UniTask Cooldown()
        {
            _isCanAttack = false;
            
            await UniTask.Delay((int)(Stats.CooldownTime * 1000));
            
            _isCanAttack = true;
        }
    }
}
