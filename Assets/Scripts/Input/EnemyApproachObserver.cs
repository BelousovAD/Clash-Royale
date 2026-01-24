using Behaviour;
using ChangeableValue;
using UnityEngine;

namespace Input
{
    public class EnemyApproachObserver : MonoBehaviour, IEnable, IDisable
    {
        [SerializeField] private Transform _transformFrom;
        [SerializeField][Min(0f)] private float _closeDistance;

        private readonly IsTransformClose _isClose = new();
        private readonly IsTransformFar _isFar = new();

        public ChangeableValue<bool> IsClose => _isClose;

        public ChangeableValue<bool> IsFar => _isFar;

        public void SetEnemy(Transform enemy) =>
            _isClose.SetDestination(enemy);

        private void Awake() =>
            _isClose.Initialize(_transformFrom, _closeDistance);

        private void Update() =>
            _isClose.Update(Time.deltaTime);

        public void Enable() =>
            _isClose.Enable();

        public void Disable() =>
            _isClose.Disable();
    }
}