using ChangeableValue;
using UnityEngine;

namespace EnemyObserve
{
    public class EnemyApproachObserver : MonoBehaviour
    {
        [SerializeField] private Transform _transformFrom;

        private readonly IsTransformClose _isClose = new();

        public ChangeableValue<bool?> IsClose => _isClose;

        public void Initialize(float closeDistance) =>
            _isClose.Initialize(closeDistance);

        public void SetEnemy(Transform enemy, float enemyRadius) =>
            _isClose.SetTarget(enemy, enemyRadius);

        private void Awake() =>
            _isClose.Initialize(_transformFrom);

        private void OnEnable() =>
            _isClose.Enable();

        private void OnDisable() =>
            _isClose.Disable();

        private void Update() =>
            _isClose.Update(Time.deltaTime);
    }
}