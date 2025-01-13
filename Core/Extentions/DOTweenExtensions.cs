using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Core.Extentions
{
    public static class DOTweenExtensions
    {
        public static Tween Bind(this Tween tween,object id)
        {
            return tween.SetId(id).SetUpdate(false);
        }
        
        public static Tween Bind(this Tween tween, GameObject link, object id)
        {
            return tween.SetLink(link).SetId(id).SetUpdate(false);
        }
        
        public static Tween DOJumpX(this Transform target, Vector3 destination, float jumpPower, float moveDuration, float jumpDuration, Ease moveEase = Ease.InBack)
        {
            target.DOMoveY(destination.y, moveDuration).SetEase(moveEase);
            target.DOMoveZ(destination.z, moveDuration).SetEase(moveEase);
            
            var sequence = DOTween.Sequence();
            sequence.Append(target.DOMoveX(target.position.x + jumpPower, jumpDuration).SetEase(Ease.InOutSine));
            sequence.Append(target.DOMoveX(destination.x, moveDuration - jumpDuration).SetEase(Ease.InOutSine));
            return sequence;
        }

        public static Tween ConfigureWithId(this Tween tween, object id, GameObject gameObject)
        {
            return tween.SetUpdate(false).SetId(id).SetLink(gameObject);
        }
        
        public static Tween RelativeMove(this Transform target, Transform destination, Vector3 localDelta, float duration)
        {
            Vector3 startPosition = target.position;
            return DOVirtual.Float(0, 1, duration, value =>
            {
                target.position = Vector3.Lerp(startPosition, destination.position + localDelta, value);
            });
        }

        public static Tween Jump(this Transform target, Vector3 jump, float duration)
        {
            Vector3 startPosition = target.transform.localPosition;
            var sequence = DOTween.Sequence();
            sequence.Append(target.DOLocalMove(startPosition + jump, duration / 2).SetEase(Ease.InOutSine));
            sequence.Append(target.DOLocalMove(startPosition, duration / 2).SetEase(Ease.InOutSine));
            sequence.SetUpdate(false);
            return sequence;
        }

        public static Tween JumpUpWithScale(this Transform target, Vector3 positionPunch, float targetScale,
            float inDuration, float outDuration, float endScale = 1.0f,  Ease ease = Ease.InBack)
        {
            var sequence = DOTween.Sequence();
            var startPosition = target.localPosition;
            sequence.Append(target.MoveUpWithZoomIn(positionPunch, targetScale, inDuration));
            sequence.Append(target.MoveWithZoomOut(startPosition, endScale, outDuration, ease));
            return sequence;
        }
        
        public static Tween MoveUpWithZoomIn(this Transform target, Vector3 positionPunch, float targetScale, float duration)
        {
            var startPosition = target.localPosition;
            var sequence = DOTween.Sequence();
            sequence.Append(target.DOLocalMove(startPosition + positionPunch,
                duration).SetEase(Ease.OutBack));
            sequence.Join(target.DOScale(targetScale, duration));
            return sequence;
        }
        
        public static Tween MoveWithZoomOut(this Transform target, Vector3 targetPosition, float targetScale, float duration, Ease ease = Ease.InBack)
        {
            var sequence = DOTween.Sequence();
            sequence.Append(target.DOLocalMove(targetPosition,
                duration).SetEase(Ease.InQuad));
            sequence.Join(target.DOScale(targetScale, duration).SetEase(Ease.InBack));
            return sequence;
        }

        public static Tween DoCounter(this TMP_Text text, int start, int end, float duration, Action<int> onTextChanged = null)
        {
            return DOVirtual.Int(start, end, duration, value =>
            {
                text.text = value.ToString();
                onTextChanged?.Invoke(value);
            });
        }

        public static Tween DoSmoothRotate(this Transform target, float rotationSpeed, float duration)
        {
            return DOVirtual.Float(0, 1, duration, _ =>
            {
                target.localRotation *= Quaternion.Euler(Vector3.one * (rotationSpeed * Time.deltaTime));
            }).SetUpdate(false);
        }

    }
}