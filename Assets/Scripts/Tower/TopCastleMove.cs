using DG.Tweening;
using UnityEngine;

namespace Tower
{
    public class TopCastleMove : MonoBehaviour
    {
        private const float Duration = 1;
        private const float Delay = 1.88f;
        
        [SerializeField] private Transform _endPoint;
        
        private Sequence _sequence;

        private void Start()
        {
            Move();
        }

        private void OnDisable()
        {
            _sequence?.Kill();
        }

        private void Move()
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOMove(_endPoint.position, Duration)).SetDelay(Delay);
            _sequence.Join(transform.DORotate(_endPoint.rotation.eulerAngles, Duration).SetDelay(Delay));
        }
    }
}