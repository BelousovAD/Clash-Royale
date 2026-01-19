using Item;
using UnityEngine;
using UnityEngine.UI;

namespace Card
{
    [RequireComponent(typeof(CanvasGroup))]
    internal class CardLockView : ItemView<Card>
    {
        private const float MinAlpha = 0f;
        private const float MaxAlpha = 1f;

        [SerializeField] private Toggle _toggle;
        
        private CanvasGroup _canvasGroup;

        private void Awake() =>
            _canvasGroup = GetComponent<CanvasGroup>();

        protected override void Subscribe() =>
            Item.LockStatusChanged += UpdateView;

        protected override void Unsubscribe() =>
            Item.LockStatusChanged -= UpdateView;

        protected override void UpdateView()
        {
            if (Item is not null && Item.IsLocked == false)
            {
                _toggle.interactable = true;
                _canvasGroup.alpha = MinAlpha;
                _canvasGroup.blocksRaycasts = false;
                _canvasGroup.interactable = false;
            }
            else
            {
                _toggle.interactable = false;
                _canvasGroup.alpha = MaxAlpha;
                _canvasGroup.blocksRaycasts = true;
                _canvasGroup.interactable = true;
            }
        }
    }
}