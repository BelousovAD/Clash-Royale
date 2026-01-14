using System.Collections;
using System.Collections.Generic;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Currency
{
    [RequireComponent(typeof(Slider))]
    internal class CurrencySliderView : MonoBehaviour
    {
        [SerializeField] private CurrencyType _currencyType;
        [SerializeField][Min(0)] private float _changingTime = 2;
        [SerializeField] private int _valueOffset = 1; 

        private float _changingSpeed;
        private float _targetValue = 0.5f;
        private Coroutine _changingValueSmoothly;
        private Currency _currency;
        private Slider _slider;

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
            _slider = GetComponent<Slider>();

        private void OnEnable()
        {
            _slider.value = _targetValue;
            _currency.Changed += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _currency.Changed -= UpdateView;

        private void UpdateView()
        {
            _targetValue = ((float)_currency.Value + _valueOffset) / _currency.Max;
            _changingSpeed = Mathf.Abs(_targetValue - _slider.value) / _changingTime;
            _changingValueSmoothly ??= StartCoroutine(ChangeValueSmoothly());
        }

        private IEnumerator ChangeValueSmoothly()
        {
            while (isActiveAndEnabled && IsTargetReached() == false)
            {
                _slider.value = Mathf.MoveTowards(_slider.value, _targetValue, Time.deltaTime * _changingSpeed);

                yield return null;
            }

            _slider.value = _targetValue;
            _changingValueSmoothly = null;
        }
        
        private bool IsTargetReached() =>
            Mathf.Approximately(_targetValue, _slider.value);
    }
}