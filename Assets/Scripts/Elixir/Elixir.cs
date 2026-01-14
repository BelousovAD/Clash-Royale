using System;
using System.Collections;
using Bootstrap;
using Currency;
using UnityEngine;

namespace Elixir
{
    internal class Elixir : Currency.Currency, IDisposable
    {
        private readonly int _earnAmount;
        private readonly WaitForSecondsRealtime _wait;
        
        private SavvyServicesProvider _services;
        private Coroutine _coroutine;
        private bool _isEnable;

        public Elixir(CurrencyType type, int defaultValue, int max, float earningTime, int earnAmount)
            : base(type, defaultValue, max)
        {
            _earnAmount = earnAmount;
            _wait = new WaitForSecondsRealtime(earningTime);
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
                Earn(_earnAmount);

                yield return _wait;
            }
        }
    }
}