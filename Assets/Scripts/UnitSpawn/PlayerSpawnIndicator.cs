using Reflex.Attributes;
using UnityEngine;

namespace UnitSpawn
{
    public class PlayerSpawnIndicator : MonoBehaviour
    {
        private static readonly Vector3 DefaultPosition = new (0, -5, 0);

        [SerializeField] private Indicator _indicatorInstance;

        private Transform _transform;
        private RayPointer.RayPointer _rayPointer;

        [Inject]
        private void Initialize(RayPointer.RayPointer rayPointer) =>
            _rayPointer = rayPointer;

        private void Awake() =>
            _transform = transform;

        private void OnEnable()
        {
            _rayPointer.Dragging += MoveIndicator;
            _rayPointer.DragEnded += TurnOffIndicator;
        }

        private void OnDisable()
        {
            _rayPointer.Dragging -= MoveIndicator;
            _rayPointer.DragEnded -= TurnOffIndicator;
        }

        private void MoveIndicator()
        {
            _transform.position = _rayPointer.Position;
            _indicatorInstance.SetPositionToSpawn(_rayPointer.Position);
        }

        private void TurnOffIndicator() =>
            _transform.position = DefaultPosition;
    }
}