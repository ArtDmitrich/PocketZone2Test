using GameLogic.Weapons;
using UnityEngine;

namespace GameLogic.Characters
{
    public enum ModelAnimation
    {
        Resurrection,
        Walk,
        Death,
        Hurt
    }
    
    [RequireComponent(typeof(Animator))]
    public class CharacterModel : MonoBehaviour
    {
        [SerializeField] private Transform _weaponSlot;
        
        private Animator Anim { get { return _anim ??= GetComponent<Animator>(); } }
        private Animator _anim;

        private Transform _currentWeapon;

        public void PlayShortAnimation(ModelAnimation animation)
        {
            Anim.SetTrigger(animation.ToString());
        }
        
        public void PlayLoopAnimation(ModelAnimation animation, bool loop)
        {
            Anim.SetBool(animation.ToString(), loop);
        }

        public void SetWeapon(Transform weapon)
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.parent = null;
            }

            _currentWeapon = weapon;
            _currentWeapon.parent = _weaponSlot;
            _currentWeapon.localPosition = Vector3.zero;
            _currentWeapon.localScale = Vector3.one;
        }
    }
}
