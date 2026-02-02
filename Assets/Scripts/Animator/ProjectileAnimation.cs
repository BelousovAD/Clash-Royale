using UnityEngine;

namespace Animator
{
    internal class ProjectileAnimation : MonoBehaviour
    {
        private const float Speed = 500;
        
        [SerializeField] private GameObject _projectile;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private Transform _target;

        public void SendProjectile()
        {
            GameObject projectile = Instantiate(_projectile, _spawnPosition.position, Quaternion.identity) as GameObject;
            //projectile.transform.LookAt(_target);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * Speed);
        }
    }
}