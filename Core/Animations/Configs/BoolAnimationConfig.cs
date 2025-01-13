using UnityEngine;

namespace Core.Animations
{
    [CreateAssetMenu(menuName = "Animations/Bool")]
    public class BoolAnimationConfig : AnimationConfig
    {
        public AnimationType type => AnimationType.Bool;
        
        public void Apply(Animator animator)
        {
            animator.SetBool(animationName, true);
        }

        public void Apply(Animator animator, bool value) => animator.SetBool(animationName, value);
    }
}