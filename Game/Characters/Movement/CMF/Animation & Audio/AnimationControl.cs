using Game.Characters.Api;
using Core.Animations;
using NaughtyAttributes;
using UnityEngine;

namespace CMF
{
	public class AnimationControl : MonoBehaviour
	{
		[SerializeField] private bool _useStrafeAnimations = false;
		
		[SerializeField] private FloatAnimationConfig _verticalSpeedAnimation;
		[SerializeField] private FloatAnimationConfig _horizontalSpeedAnimation;

		[SerializeField, ShowIf(nameof(_useStrafeAnimations))] private FloatAnimationConfig _forwardSpeedAnimation;
		[SerializeField, ShowIf(nameof(_useStrafeAnimations))] private FloatAnimationConfig _strafedSpeedAnimation;

		[SerializeField] private BoolAnimationConfig _isGroundedAnimation;
		[SerializeField] private BoolAnimationConfig _isStrafingAnimation;

		[SerializeField] private TriggerAnimationConfig _onLandAnimation;
		
		private IAnimatorUser _animatorUser;
		public IAnimatorUser animatorUser => _animatorUser ??= GetComponentInParent<IAnimatorUser>(true);
		
		private Controller _controller;
		private Controller controller => _controller ??= GetComponentInParent<Controller>();
		
		private Animator animator => animatorUser.animator;
		private Transform animatorTransform => animator.transform;

		public float landVelocityThreshold = 5f;

		private float smoothingFactor = 40f;
		Vector3 oldMovementVelocity = Vector3.zero;

		void OnEnable()
		{
			controller.OnLand += OnLand;
			controller.OnJump += OnJump;
		}

		void OnDisable()
		{
			controller.OnLand -= OnLand;
			controller.OnJump -= OnJump;
		}
		
		void Update () {

			Vector3 velocity = controller.GetVelocity();

			Vector3 horizontalVelocity = VectorMath.RemoveDotVector(velocity, controller.transform.up);
			Vector3 verticalVelocity = velocity - horizontalVelocity;

			horizontalVelocity = Vector3.Lerp(oldMovementVelocity, horizontalVelocity, smoothingFactor * Time.deltaTime);
			oldMovementVelocity = horizontalVelocity;

			float verticalSpeed = verticalVelocity.magnitude *
			                      VectorMath.GetDotProduct(verticalVelocity.normalized, controller.transform.up);
			_verticalSpeedAnimation.Apply(animator, verticalSpeed);
			
			float horizontalSpeed = horizontalVelocity.magnitude;
			_horizontalSpeedAnimation.Apply(animator, horizontalSpeed);
			
			if(_useStrafeAnimations)
			{
				Vector3 localVelocity = animatorTransform.InverseTransformVector(horizontalVelocity);
				_forwardSpeedAnimation.Apply(animator, localVelocity.z);
				_strafedSpeedAnimation.Apply(animator, localVelocity.x);
			}

			_isGroundedAnimation.Apply(animator, controller.IsGrounded());
			_isStrafingAnimation.Apply(animator, _useStrafeAnimations);
		}

		void OnLand(Vector3 _v)
		{
			if(VectorMath.GetDotProduct(_v, controller.transform.up) > -landVelocityThreshold)
				return;

			_onLandAnimation.Apply(animator);
		}

		void OnJump(Vector3 _v)
		{
			
		}
	}
}
