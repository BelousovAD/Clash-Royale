using DG.Tweening;
using UnityEngine;

namespace SearchOpponent
{
    internal class LoupeAnimator : MonoBehaviour
    {
        private const float Degrees = 360f;
        private const Ease Ease = DG.Tweening.Ease.Linear;
        private const int InfiniteLoops = -1;
        
        [SerializeField][Min(0f)] private float _radius = 30;
        [SerializeField][Min(0f)] private float _circleDuration = 2;
        
        private RectTransform _rectTransform;
        private Tweener _tweener;

        private void Awake() =>
            _rectTransform = GetComponent<RectTransform>();

        private void OnEnable()
        {
            _rectTransform.anchoredPosition = new Vector2(0f, _radius);
            _tweener = _rectTransform
                .DOShapeCircle(Vector2.zero, Degrees, _circleDuration)
                .SetLoops(InfiniteLoops)
                .SetEase(Ease);
        }

        private void OnDisable() =>
            _tweener?.Kill();
    }
}