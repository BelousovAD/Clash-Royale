using UnityEngine;

namespace ChestUnlockAcceleration
{
    internal class ChestAccelerationButtonView : MonoBehaviour
    {
        private const int Min = 0;
        
        [SerializeField] private ChestAccelerationButton _button;
        [SerializeField] private ChestAccelerationPriceCalculator _priceCalculator;

        private void OnEnable()
        {
            _priceCalculator.PriceChanged += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _priceCalculator.PriceChanged -= UpdateView;

        private void UpdateView() =>
            _button.gameObject.SetActive(_priceCalculator.Price > Min);
    }
}