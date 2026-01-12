using DG.Tweening;
using Reflex.Attributes;
using UnityEngine;

namespace Chest
{
    internal class ChestAnimator : MonoBehaviour
    {
        private const Ease EaseType = Ease.OutBounce;

        [SerializeField] private Transform _lidTransform;
        [SerializeField][Range(0, 180)] private float _openingAngle = 120f;
        [SerializeField][Min(0)] private float _animationDuration;

        private Tweener _tweener;
        private CardUnlocker _cardUnlocker;

        [Inject]
        private void Initialize(CardUnlocker cardUnlocker) =>
            _cardUnlocker = cardUnlocker;

        private void OnEnable() =>
            _cardUnlocker.CardUnlocked += OpenChest;

        private void OnDisable()
        {
            _cardUnlocker.CardUnlocked -= OpenChest;
            _tweener?.Kill();
        }

        private void OpenChest(Card.Card card) =>
            _tweener = _lidTransform
                .DOLocalRotate(new Vector3(_openingAngle, 0, 0), _animationDuration)
                .SetEase(EaseType);
    }
}