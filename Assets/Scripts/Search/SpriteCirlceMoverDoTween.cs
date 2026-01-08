using UnityEngine;
using DG.Tweening;

namespace Search
{
    public class SpriteCirlceMoverDoTween : MonoBehaviour
    {
        private readonly Ease _ease = Ease.Linear;
        private readonly int _infiniteLoops = -1;

        [SerializeField] private float _duration = 1f;
        [SerializeField] private float _radius = 1f;
        [SerializeField] private bool _clockLike = false;

        private Vector2 _centerPosition;
        private RectTransform _rectTransform;
        private Tween _moveTween;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            SetupCenter();
            StartMoving();
        }

        private void OnDisable()
        {
            _moveTween?.Kill();
        }

        private void SetupCenter()
        {
            _centerPosition = _rectTransform.anchoredPosition;
        }

        private void StartMoving()
        {
            if (_moveTween != null && _moveTween.IsActive())
            {
                _moveTween.Kill();
            }

            float angle = 0f;
            Vector2 startPosition = SetRadius(angle);
            _rectTransform.anchoredPosition = startPosition;

            _moveTween = DOVirtual.Float(0f, 360f, _duration, (currentAngle) =>
                {
                    if (!_clockLike) currentAngle = -currentAngle;
                    Vector2 newPosition = SetRadius(currentAngle);
                    _rectTransform.anchoredPosition = newPosition;
                }).SetEase(_ease)
                .SetLoops(_infiniteLoops, LoopType.Restart);
        }

        private Vector2 SetRadius(float angle)
        {
            float angledRadius = angle * Mathf.Deg2Rad;

            float x = _centerPosition.x + Mathf.Cos(angledRadius) * _radius;
            float y = _centerPosition.y + Mathf.Sin(angledRadius) * _radius;

            return new Vector2(x, y);
        }
    }
}