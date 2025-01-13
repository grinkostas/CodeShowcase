using DG.Tweening;
using Game.Characters.Core;
using UnityEngine;

namespace Game.Interact.Items
{
    public class DownThrowGrenade : GrenadeItem
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private GameObject _model;
        [SerializeField] private float _autoExpolodeDelay;
        
        private bool _exploded = false;
        private bool _throwed = false;

        private Tween _autoExplodeTween;
        
        private void Awake()
        {
            SetActivePhysics(false);
            _particle.Stop();
        }

        public override void PrepareForThrow()
        {
            _throwed = true;
        }

        public override void Throw()
        {
            SetActivePhysics(true);
            _autoExplodeTween = DOVirtual.DelayedCall(_autoExpolodeDelay, Explode).SetUpdate(false).SetId(this).SetLink(gameObject);
            OnThrow();
        }
        
        protected virtual void OnThrow(){}
        
        private void OnCollisionEnter(Collision other)
        {
            if(_throwed == false)
                return;
            if(_exploded)
                return;
            if(other.collider.TryGetComponent(out Character character))
                return;
            Explode();
        }

        private void SetActivePhysics(bool isActive)
        {
            rb.constraints = isActive ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeAll;
            col.enabled = isActive;
        }

        private void Explode()
        {
            if(_exploded)
                return;
            _autoExplodeTween?.Kill();
            _exploded = true;
            SetActivePhysics(false);
            _particle.Play();
            _model.SetActive(false);
            OnExplode();
        }

        protected virtual void OnExplode(){}
    }
}