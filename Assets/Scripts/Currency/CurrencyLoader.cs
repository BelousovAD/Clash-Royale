namespace Currency
{
    using Bootstrap;
    using Reflex.Attributes;
    using UnityEngine;

    internal class CurrencyLoader : MonoBehaviour, ILoadable
    {
        private Money _money;

        [Inject]
        private void Initialize(Money money) =>
            _money = money;

        public void Load() =>
            _money.Load();
    }
}