using UnityEngine;

namespace UnitHealth
{
    internal class WorldRotationKeeper : MonoBehaviour
    {
        [SerializeField] private Vector3 _rotation;

        private Transform _transform;
        private Quaternion _quaternion;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _quaternion = Quaternion.Euler(_rotation);
        }

        private void Update() =>
            _transform.rotation = _quaternion;
    }
}