using Character;
using EnemyObserve;
using UnityEngine;

namespace UnitInitialize
{
    internal class FromCharacterDataInitializer : MonoBehaviour
    {
        [SerializeField] private CharacterData _data;
        [SerializeField] private EnemyApproachObserver _approachObserver;

        private void OnEnable() =>
            Initialize();

        private void Initialize() =>
            _approachObserver.Initialize(_data.AttackRange);
    }
}