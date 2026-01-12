using DG.Tweening;
using Reflex.Attributes;
using UnityEngine;

namespace Chest
{
    public class UnlockedCardView : MonoBehaviour
    {
        private readonly Vector3 _minScale = Vector3.zero;
        private readonly Vector3 _maxScale = Vector3.one;
        
        [SerializeField] private CanvasGroup _windowCanvasGroup;
        [SerializeField] private CanvasGroup _cardCanvasGroup;
        [SerializeField][Min(0f)] private float _animationDelay;
        [SerializeField][Min(0F)] private float _animationDuration;
        
        private CardUnlocker _cardUnlocker;

        [Inject]
        private void Initialize(CardUnlocker cardUnlocker) =>
            _cardUnlocker = cardUnlocker;

        private void OnEnable()
        {
            _cardUnlocker.CardUnlocked += UpdateView;
            _cardCanvasGroup.transform.localScale = _minScale;
        }

        private void OnDisable() =>
            _cardUnlocker.CardUnlocked -= UpdateView;

        private void UpdateView(Card.Card card)
        {
            _windowCanvasGroup.interactable = false;
            _cardCanvasGroup.interactable = false;
            _cardCanvasGroup.transform.localScale = _minScale;
            DOTween.Sequence()
                .AppendInterval(_animationDelay)
                .Append(_cardCanvasGroup.transform.DOScale(_maxScale, _animationDuration))
                .AppendCallback(() => _cardCanvasGroup.interactable = true);
        }
    }
}