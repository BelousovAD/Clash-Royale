using UnityEngine;

namespace SearchOpponent
{
    internal class Joint : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        private void LateUpdate() =>
            transform.position = _target.position;
    }
}