using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(Animator))]
    public class WeaponModel : MonoBehaviour
    {
        [SerializeField] private string _animatorAttackTriggerName;
        private Animator Anim { get { return _anim ??= GetComponent<Animator>(); } }
        private Animator _anim;


        public void PlayAttackAnimation()
        {
            Anim.SetTrigger(_animatorAttackTriggerName);
        }
    }
}