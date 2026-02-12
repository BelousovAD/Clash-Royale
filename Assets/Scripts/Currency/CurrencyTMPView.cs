using System.Collections.Generic;
using Reflex.Attributes;
using Savvy.Extensions;
using TMPro;
using UnityEngine;

namespace Currency
{
    [RequireComponent(typeof(TMP_Text))]
    internal class CurrencyTMPView : MonoBehaviour
    {
        [SerializeField] private string _format = "{0}";
        [SerializeField] private CurrencyType _currencyType;

        private TMP_Text _textField;
        private Currency _currency;

        [Inject]
        private void Initialize(IEnumerable<Currency> currencies)
        {
            foreach (Currency currency in currencies)
            {
                if (currency.Type == _currencyType)
                {
                    _currency = currency;
                    break;
                }
            }
        }

        private void Awake() =>
            _textField = GetComponent<TMP_Text>();

        private void OnEnable()
        {
            _currency.Changed += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _currency.Changed -= UpdateView;

        private void UpdateView() =>
            _textField.text = string.Format(_format, _currency.Value.ToNumsFormat());
    }
}