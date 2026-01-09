using Item;
using UnityEngine;
using UnityEngine.UI;

namespace Card
{
    [RequireComponent(typeof(CanvasGroup))]
    internal class CardLockView : ItemView
    {
        private const float MinAlpha = 0f;
        private const float MaxAlpha = 1f;

        [SerializeField] private Toggle _toggle;
        
        private CanvasGroup _canvasGroup;

        protected new Card Item { get; private set; }

        private void Awake() =>
            _canvasGroup = GetComponent<CanvasGroup>();

        public override void UpdateView()
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

        protected override void UpdateSubscriptions()
        {
            Unsubscribe();
            Item = base.Item as Card;
            Subscribe();
            base.UpdateSubscriptions();
        }

        private void Subscribe()
        {
            if (Item is not null)
            {
                Item.LockStatusChanged += UpdateView;
            }
        }

        private void Unsubscribe()
        {
            if (Item is not null)
            {
                Item.LockStatusChanged -= UpdateView;
            }
        }
    }
}