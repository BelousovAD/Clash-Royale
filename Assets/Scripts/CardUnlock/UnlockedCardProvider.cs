using Item;
using Reflex.Attributes;

namespace CardUnlock
{
    internal class UnlockedCardProvider : ItemProvider
    {
        private CardUnlocker _cardUnlocker;

        [Inject]
        private void Initialize(CardUnlocker cardUnlocker) =>
            _cardUnlocker = cardUnlocker;

        private void OnEnable() =>
            _cardUnlocker.CardUnlocked += Initialize;

        private void OnDisable() =>
            _cardUnlocker.CardUnlocked -= Initialize;
    }
}