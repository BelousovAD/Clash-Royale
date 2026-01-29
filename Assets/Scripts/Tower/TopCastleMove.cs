using DG.Tweening;
using UnityEngine;

namespace Tower
{
    public class TopCastleMove : MonoBehaviour
    {
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
            _sequence.Append(transform.DOMove(_endPoint.position, 2));
            _sequence.Append(transform.DORotate(_endPoint.rotation.eulerAngles, 2));
        }
    }
}