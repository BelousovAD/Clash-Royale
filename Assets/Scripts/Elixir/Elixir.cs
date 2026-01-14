using System;
using System.Collections;
using Bootstrap;
using Currency;
using UnityEngine;

namespace Elixir
{
    internal class Elixir : Currency.Currency, IDisposable
    {
        private readonly float _timeToEarn;
        private readonly int _elixirToEarn;
        
        private SavvyServicesProvider _services;
        private Coroutine _coroutine;
        private WaitForSecondsRealtime _wait;
        private bool _isEnable;

        public Elixir(CurrencyType type, int defaultValue, int max, float timeToEarn, int elixirToEarn)
            : base(type, defaultValue, max)
        {
            _timeToEarn = timeToEarn;
            _elixirToEarn = elixirToEarn;
            _wait = new WaitForSecondsRealtime(_timeToEarn);
            _isEnable = true;
        }
        
        public void Dispose()
        {
            _isEnable = false;
            StopEarning();
        }

        public void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;

        public void StartEarning() =>
            _coroutine = _services.CoroutineRunner.StartCoroutine(EarnRoutine());

        private void StopEarning() =>
            _services.CoroutineRunner.StopCoroutine(_coroutine);

        private IEnumerator EarnRoutine()
        {
            while (_isEnable)
            {
                Earn(_elixirToEarn);

                yield return _wait;
            }
        }
    }
}