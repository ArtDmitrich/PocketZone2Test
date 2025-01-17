using UnityEngine;

namespace Characters
{
    public enum ModelAnimation
    {
        Walk,
        Death,
        Attack,
        Hurt
    }
    
    [RequireComponent(typeof(Animator))]
    public class CharacterModel : MonoBehaviour
    {
        private Animator Anim { get { return _anim ??= GetComponent<Animator>(); } }
        private Animator _anim;

        public void PlayShortAnimation(ModelAnimation animation)
        {
            Anim.SetTrigger(animation.ToString());
        }
        
        public void PlayLoopAnimation(ModelAnimation animation, bool loop)
        {
            Anim.SetBool(animation.ToString(), loop);
        }
    }
}
