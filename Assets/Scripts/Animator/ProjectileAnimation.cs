using UnityEngine;

namespace Animator
{
    internal class ProjectileAnimation : MonoBehaviour
    {
        private const float Speed = 1500;

        [SerializeField] private GameObject _projectile;
        [SerializeField] private Transform _spawnPosition;

        public void SendProjectile()
        {
            GameObject projectile = Instantiate(_projectile, _spawnPosition.position, Quaternion.identity) as GameObject;
            
            if (projectile.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.AddForce(projectile.transform.forward * Speed);
            }
        }
    }
}