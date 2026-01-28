using UnityEngine;

namespace RayPointer
{
    public class SpawnIndicator : MonoBehaviour
    {
        private static readonly Vector3 DefaultPosition = new (0, -5, 0);

        [SerializeField] private Indicator _indicatorInstance;

        private Transform _transform;

        private void Awake() =>
            _transform = transform;

        public void MoveIndicator(Vector3 targetPosition) =>
            _transform.position = targetPosition;

        public void TurnOffIndicator()
        {
            _indicatorInstance.SetPositionToSpawn(_transform.position);
            _transform.position = DefaultPosition;
        }
    }
}