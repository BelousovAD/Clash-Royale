using System.Collections.Generic;
using Reflex.Core;
using Unit;
using UnitSpawn;
using UnityEngine;

namespace EnemyFind
{
    internal class EnemyFindInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private UnitSpawner _enemyUnitSpawner;
        [SerializeField] private UnitSpawner _playerUnitSpawner;
        [SerializeField] private List<Unit.Unit> _enemyTowers;
        [SerializeField] private List<Unit.Unit> _playerTowers;
        
        private ClosestUnitFinder _enemyUnitFinder;
        private ClosestUnitFinder _playerUnitFinder;
        private ContainerBuilder _builder;

        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _enemyUnitFinder = new ClosestUnitFinder(UnitType.Enemy);
            _playerUnitFinder = new ClosestUnitFinder(UnitType.Player);

            _builder.AddSingleton(_enemyUnitFinder);
            _builder.AddSingleton(_playerUnitFinder);

            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Container container)
        {
            _builder.OnContainerBuilt -= Initialize;

            _enemyUnitFinder.Initialize(_enemyUnitSpawner, _enemyTowers);
            _playerUnitFinder.Initialize(_playerUnitSpawner, _playerTowers);
        }
    }
}