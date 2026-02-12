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
        
        [SerializeField] private ChestAccelerationPriceCalculator _priceCalculator;
        [SerializeField] private GameObject _priceAdView;
        [SerializeField] private GameObject _priceView;
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
                _priceView.SetActive(false);
            }
            else
            {
                _priceAdView.SetActive(false);
                _priceView.SetActive(true);
            }
        }
    }
}