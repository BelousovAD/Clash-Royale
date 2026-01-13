using DG.Tweening;
using Reflex.Attributes;
using UnityEngine;

namespace CardUnlock
{
    [RequireComponent(typeof(RectTransform))]
    public class ChestViewAnimator : MonoBehaviour
    {
        private const Ease EaseType = Ease.OutQuint;
        private const int LoopsCount = 2;

        [SerializeField][Min(0)] private float _chestScale = 1.5f;
        [SerializeField][Min(0)] private float _animationDuration;

        private RectTransform _imageRectTransform;
        private CardUnlocker _cardUnlocker;
        private Sequence _sequence;

        [Inject]
        private void Initialize(CardUnlocker cardUnlocker) =>
            _cardUnlocker = cardUnlocker;

        private void Start() =>
            _imageRectTransform = GetComponent<RectTransform>();

        private void OnEnable() =>
            _cardUnlocker.CardUnlocked += OpenChest;

        private void OnDisable()
        {
            _cardUnlocker.CardUnlocked -= OpenChest;
            _sequence?.Kill();
        }

        private void OpenChest(Card.Card card)
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence().Append(_imageRectTransform
                .DOScale(new Vector3(_chestScale, _chestScale, _chestScale), _animationDuration)
                .SetEase(EaseType)
                .SetLoops(LoopsCount, LoopType.Yoyo));
        }
    }
}