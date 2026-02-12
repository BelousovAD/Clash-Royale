using System;
using System.Collections.Generic;
using Currency;
using Reflex.Attributes;
using UnityEngine;

namespace Elixir
{
    internal class ElixirEarner : MonoBehaviour
    {
        private const CurrencyType ElixirCurrency = CurrencyType.Elixir;
        private const CurrencyType EnemyElixirCurrency = CurrencyType.EnemyElixir;
        private const int EarnAmount = 1;
        private const float MinValue = 0f;
        private const float MaxValue = 1f;

        [SerializeField] private float _delay = 2f;
        [SerializeField] private bool _isForEnemy;

        private Currency.Currency _currency;
        private float _progress;
        private float _speed;

        public event Action ProgressChanged;

        public float Progress
        {
            get
            {
                return _progress;
            }

            private set
            {
                _progress = Mathf.Clamp(value, MinValue, MaxValue);
                ProgressChanged?.Invoke();
            }
        }

        [Inject]
        private void Initialize(IEnumerable<Currency.Currency> currencies)
        {
            foreach (Currency.Currency currency in currencies)
            {
                if (_isForEnemy == false && currency.Type == ElixirCurrency
                    || _isForEnemy && currency.Type == EnemyElixirCurrency)
                {
                    _currency = currency;
                    break;
                }
            }
        }

        private void Awake() =>
            _speed = 1 / _delay;

        private void Update()
        {
            if (_currency.Value < _currency.Max)
            {
                Progress += Time.deltaTime * _speed;

                if (Mathf.Approximately(Progress, MaxValue))
                {
                    _currency.Earn(EarnAmount);
                    Progress = MinValue;
                }
            }
        }
    }
}