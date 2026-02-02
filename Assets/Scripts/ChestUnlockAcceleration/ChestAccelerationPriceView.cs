using System.Collections.Generic;
using Currency;
using Reflex.Attributes;
using TMPro;
using UnityEngine;

namespace ChestUnlockAcceleration
{
    internal class ChestAccelerationPriceView : MonoBehaviour
    {
        private const CurrencyType MoneyCurrency = CurrencyType.Money;
        private static readonly Color AvailableColor = Color.white;
        private static readonly Color UnavailableColor = Color.red;
        
        [SerializeField] private ChestAccelerationPriceCalculator _priceCalculator;
        [SerializeField] private GameObject _priceAdView;
        [SerializeField] private TMP_Text _textField;
        [SerializeField] private string _format = "{0}";

        private Currency.Currency _currency;

        [Inject]
        private void Initialize(IEnumerable<Currency.Currency> currencies)
        {
            foreach (Currency.Currency currency in currencies)
            {
                if (currency.Type == MoneyCurrency)
                {
                    _currency = currency;
                    break;
                }
            }
        }

        private void OnEnable()
        {
            _priceCalculator.PriceChanged += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _priceCalculator.PriceChanged -= UpdateView;

        private void UpdateView()
        {
            _textField.text = string.Format(_format, _priceCalculator.Price);

            if (_currency.Value < _priceCalculator.Price)
            {
                _priceAdView.SetActive(true);
                _textField.color = UnavailableColor;
            }
            else
            {
                _priceAdView.SetActive(false);
                _textField.color = AvailableColor;
            }
        }
    }
}