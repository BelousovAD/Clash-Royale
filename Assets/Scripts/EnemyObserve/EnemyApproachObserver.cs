using Behaviour;
using ChangeableValue;
using UnityEngine;

namespace EnemyObserve
{
    public class EnemyApproachObserver : MonoBehaviour, IEnable, IDisable
    {
        [SerializeField] private Transform _transformFrom;
        [SerializeField][Min(0f)] private float _closeDistance;
        [SerializeField] private Transform _enemy;

        private readonly IsTransformClose _isClose = new();

        public ChangeableValue<bool?> IsClose => _isClose;

        public void SetEnemy(Transform enemy) =>
            _isClose.SetDestination(enemy);

        private void Awake()
        {
            _isClose.Initialize(_transformFrom, _closeDistance);

            if (_enemy is not null)
            {
                SetEnemy(_enemy);
            }
        }

        private void Update() =>
            _isClose.Update(Time.deltaTime);

        public void Enable() =>
            _isClose.Enable();

        public void Disable() =>
            _isClose.Disable();
    }
}