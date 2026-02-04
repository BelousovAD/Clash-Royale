using UnityEngine;

namespace Animator
{
    internal class ProjectileAnimation : MonoBehaviour
    {
        private const float Speed = 1000;

        [SerializeField] private GameObject _projectile;
        [SerializeField] private Transform _spawnPosition;

        public void SendProjectile()
        {
            GameObject projectile = Instantiate(_projectile, _spawnPosition.position, Quaternion.identity);
            
            if (projectile.TryGetComponent(out Rigidbody body))
            {
                body.AddForce(projectile.transform.forward * Speed);
            }
        }
    }
}