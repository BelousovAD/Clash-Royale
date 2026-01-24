using Behaviour;
using ChangeableValue;
using UnityEngine;

namespace Input
{
    public class EnemyApproachObserver : MonoBehaviour, IEnable, IDisable
    {
        [SerializeField] private Transform _transformFrom;
        [SerializeField][Min(0f)] private float _closeDistance;

        private readonly IsTransformCloseEnough _isCloseEnough = new();

        public ChangeableValue<bool> IsCloseEnough => _isCloseEnough;

        public void SetEnemy(Transform enemy) =>
            _isCloseEnough.SetDestination(enemy);

        private void Awake() =>
            _isCloseEnough.Initialize(_transformFrom, _closeDistance);

        private void Update() =>
            _isCloseEnough.Update(Time.deltaTime);

        public void Enable() =>
            _isCloseEnough.Enable();

        public void Disable() =>
            _isCloseEnough.Disable();
    }
}