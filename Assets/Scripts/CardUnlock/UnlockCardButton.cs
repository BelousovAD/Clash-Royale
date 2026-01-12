using Common;
using Reflex.Attributes;

namespace CardUnlock
{
    internal class UnlockCardButton : AbstractButton
    {
        private CardUnlocker _cardUnlocker;

        [Inject]
        private void Initialize(CardUnlocker cardUnlocker) =>
            _cardUnlocker = cardUnlocker;

        protected override void HandleClick() =>
            _cardUnlocker.UnlockCard();
    }
}