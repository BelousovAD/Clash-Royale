using DG.Tweening;
using UnityEngine;

namespace ChestAnimation
{
    [RequireComponent(typeof(Transform))]
    internal class ChestAnimation : MonoBehaviour
    {
        private const Ease EaseType = Ease.OutBounce;

        [SerializeField][Range(0, 180)] private float _openingAngle = 120f;
        [SerializeField][Min(0)] private float _animationDuration;

        private Transform _lidTransform;
        private Tweener _tweener;

        private void Awake() =>
            _lidTransform = GetComponent<Transform>();

        private void OnDisable() =>
            _tweener?.Kill();

        public void OpenChest() =>
            _tweener = _lidTransform.DOLocalRotate(new Vector3(_openingAngle, 0, 0), _animationDuration).SetEase(EaseType);
    }
}