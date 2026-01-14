using System.Collections;
using Bootstrap;
using Currency;
using UnityEngine;

namespace Elixir
{
    internal class Elixir : Currency.Currency
    {
        private readonly float _timeToEarn;
        private readonly int _elixirToEarn;

        private float _time;
        private SavvyServicesProvider _services;
        private Coroutine _coroutine;

        public Elixir(CurrencyType type, int defaultValue, int max, float timeToEarn, int elixirToEarn) 
            : base(type, defaultValue, max)
        {
            _timeToEarn = timeToEarn;
            _elixirToEarn = elixirToEarn;
        }

        public void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;

        public void StartEarning() =>
            _coroutine = _services.CoroutineRunner.StartCoroutine(ElixirEarnPerSeconds());

        public void StopEarning() =>
            _services.CoroutineRunner.StopCoroutine(_coroutine);

        private IEnumerator ElixirEarnPerSeconds()
        {
            while (true)
            {
                Earn(_elixirToEarn);

                yield return new WaitForSeconds(_timeToEarn);
            }
        }
    }
}