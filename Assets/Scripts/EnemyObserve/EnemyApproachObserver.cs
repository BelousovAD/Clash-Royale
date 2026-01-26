using ChangeableValue;
using UnityEngine;

namespace EnemyObserve
{
    public class EnemyApproachObserver : MonoBehaviour
    {
        [SerializeField] private Transform _transformFrom;
        [SerializeField][Min(0f)] private float _closeDistance;

        private readonly IsTransformClose _isClose = new();

        public ChangeableValue<bool?> IsClose => _isClose;

        public void SetEnemy(Transform enemy, float enemyRadius) =>
            _isClose.SetTarget(enemy, enemyRadius);

        private void Awake() =>
            _isClose.Initialize(_transformFrom, _closeDistance);

        private void OnEnable() =>
            _isClose.Enable();

        private void OnDisable() =>
            _isClose.Disable();

        private void Update() =>
            _isClose.Update(Time.deltaTime);
    }
}