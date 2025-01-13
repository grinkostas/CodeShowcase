using DG.Tweening;
using UnityEngine;

namespace GameCore.Core.Scripts.TransformUtilities
{
    public class LoopRotator : MonoBehaviour
    {
        [SerializeField] private bool _startOnEnable;
        [SerializeField] private Vector3 _startRotation;
        [SerializeField] private Vector3 _endRotation;
        [SerializeField] private float _duraion;
        
        private Tween _tween;
        private Tween _prepareTween;
        
        private void OnEnable()
        {
            if(_startOnEnable)
                Loop();
        }

        public void PrepareAndStartLoop()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOLocalRotate(_startRotation, 0.25f));
            sequence.AppendCallback(Loop);
            sequence.SetUpdate(false);
            _prepareTween = sequence;
        }

        public void StartLoop()
        {
            if(_tween.IsPlaying())
                return;
            if(_prepareTween.IsPlaying())
                return;
            Loop();
        }

        private void Loop()
        { 
            transform.localRotation = Quaternion.Euler(_startRotation);
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOLocalRotate(_endRotation, _duraion));
            sequence.Append(transform.DOLocalRotate(_startRotation, _duraion));
            sequence.SetLoops(-1).SetUpdate(false);
            _tween = sequence;
        }
        
        public void StopLoop()
        {
            _prepareTween?.Kill();
            _tween?.Kill();
        }

        private void OnDisable()
        {
            StopLoop();
        }
    }
}