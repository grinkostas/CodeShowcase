using UnityEngine;

namespace Game.Interact
{
    public class FollowingItem : MonoBehaviour
    {
        [SerializeField] private float smoothTime = 0.3f; 
        
        private Transform _followingPoint;
        private bool _following;
        private Vector3 _velocity = Vector3.zero;

        private void Update()
        {
            if (_following && _followingPoint != null)
            {
                transform.position = Vector3.SmoothDamp(
                    transform.position,
                    _followingPoint.position,
                    ref _velocity,
                    smoothTime
                );
            }
        }
        
        public void StartFollow(Transform point)
        {
            _followingPoint = point;
            _following = true;
        }

        public void EndFollow()
        {
            _following = false;
            _followingPoint = null;
        }
    }
}