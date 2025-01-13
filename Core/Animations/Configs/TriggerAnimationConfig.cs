using UnityEngine;

namespace Core.Animations
{
    [CreateAssetMenu(menuName = "Animations/Trigger")]
    public class TriggerAnimationConfig : AnimationConfig
    {
        public AnimationType type => AnimationType.Trigger;
       
        public void Apply(Animator animator)
        {
            animator.SetTrigger(animationName);
        }
    }
}