using UnityEngine;

namespace Core.Animations
{
    [CreateAssetMenu(menuName = "Animations/Float")]
    public class FloatAnimationConfig : AnimationConfig
    {
        public AnimationType type => AnimationType.Float;
        
        public void Apply(Animator animator, float value)
        {
            animator.SetFloat(animationName, value);
        }
    }
}