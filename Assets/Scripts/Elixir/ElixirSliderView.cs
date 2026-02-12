using System.Collections.Generic;
using Currency;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Elixir
{
    [RequireComponent(typeof(Slider))]
    internal class ElixirSliderView : MonoBehaviour
    {
        private const CurrencyType ElixirCurrencyType = CurrencyType.Elixir;

        [SerializeField] private ElixirEarner _elixirEarner;
        
        private Currency.Currency _currency;
        private Slider _slider;

        [Inject]
        private void Initialize(IEnumerable<Currency.Currency> currencies)
        {
            foreach (Currency.Currency currency in currencies)
            {
                if (currency.Type == ElixirCurrencyType)
                {
                    _currency = currency;
                    break;
                }
            }
        }

        private void Awake() =>
            _slider = GetComponent<Slider>();

        private void OnEnable()
        {
            _currency.Changed += UpdateView;
            _elixirEarner.ProgressChanged += UpdateView;
            UpdateView();
        }

        private void OnDisable()
        {
            _elixirEarner.ProgressChanged -= UpdateView;
            _currency.Changed -= UpdateView;
        }

        private void UpdateView() =>
            _slider.value = (_currency.Value + _elixirEarner.Progress) / _currency.Max;
    }
}