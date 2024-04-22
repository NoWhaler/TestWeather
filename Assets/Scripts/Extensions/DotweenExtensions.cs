using DG.Tweening;
using UnityEngine;

namespace Extensions
{
    public static class DotweenExtensions
    {
        public static void ScaleTo(this GameObject gameObject, float targetScale, float duration)
        {
            gameObject.transform.DOScale(targetScale, duration).SetEase(Ease.InOutSine);
        }
    }
}