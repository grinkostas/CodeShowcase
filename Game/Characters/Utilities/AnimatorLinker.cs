using NaughtyAttributes;
using UnityEngine;

namespace Game.Characters.Utilities
{
    public class AnimatorLinker : MonoBehaviour
    {
        [SerializeField] private bool _assigManually = false;
        [SerializeField, ShowIf(nameof(_assigManually))] private Animator _animator;

        [SerializeField, HideIf(nameof(_assigManually))] private bool _defaultParentForSearch = true;
        [SerializeField, ShowIf(nameof(isParentShow))] private Transform _parent;
        
        private Transform parent => _defaultParentForSearch ? transform.parent : _parent;
        
        public bool isParentShow => _assigManually == false && _defaultParentForSearch == false;
        public Animator animator => _assigManually ? _animator : parent.GetComponentInChildren<Animator>();
    }
}