using UnityEngine;

namespace Search
{
    public class SpriteCircleMover : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _radius = 1f;
        [SerializeField] private bool _clockLike = true;
        [SerializeField] private RectTransform _center;

        private RectTransform _rectTransform;
        private float _angle = 0f;
        private Vector2 _centerPosition;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();

            if (_center != null)
            {
                _centerPosition = _center.anchoredPosition;
            }
            else
            {
                _centerPosition = Vector2.zero;
            }
            
            UpdatePosition();
        }

        private void Update()
        {
            float direction = _clockLike ? 1f : -1f;
            _angle += _speed * direction * Time.deltaTime;
            
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            float angleInRadians = _angle * Mathf.Deg2Rad;

            float x = _centerPosition.x + Mathf.Cos(angleInRadians) * _radius;
            float y = _centerPosition.y + Mathf.Sin(angleInRadians) * _radius;

            _rectTransform.anchoredPosition = new Vector2(x, y);
        }
    }
}