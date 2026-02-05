using System.Collections.Generic;
using Currency;
using EnemyFind;
using Reflex.Attributes;
using UnitSpawn;
using UnityEngine;

namespace ArtificialOpponent
{
    internal class EnemySpawnPositionCalculator : MonoBehaviour
    {
        private const CurrencyType EnemyElixirCurrency = CurrencyType.EnemyElixir;
        
        [SerializeField] private Indicator _indicator;
        [SerializeField] private Collider _spawnField;
        [SerializeField] private EnemyFindCaller _enemyFindCaller;

        private Currency.Currency _currency;

        [Inject]
        private void Initialize(IEnumerable<Currency.Currency> currencies)
        {
            foreach (Currency.Currency currency in currencies)
            {
                if (currency.Type == EnemyElixirCurrency)
                {
                    _currency = currency;
                    break;
                }
            }
        }

        private void OnEnable()
        {
            _currency.Changed += CalculateSpawnPosition;
            CalculateSpawnPosition();
        }

        private void OnDisable() =>
            _currency.Changed -= CalculateSpawnPosition;

        private void CalculateSpawnPosition()
        {
            Vector3 enemyPosition = _enemyFindCaller.Enemy is null
                ? Vector3.zero
                : _enemyFindCaller.Enemy.transform.position;
            Vector3 closestPointOnField = _spawnField.ClosestPoint(enemyPosition);
            _indicator.SetPositionToSpawn(closestPointOnField);
        }
    }
}