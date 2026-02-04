using UnityEngine;

namespace Projectile
{
    internal class Projectile : MonoBehaviour
    {
        private const float Time = 0.3f;
        private const float MuzzleDestroy = 0.1f;
        
        [SerializeField] private GameObject _projectileParticle;
        [SerializeField] private GameObject _muzzleParticle;

        private GameObject _impact;
        private GameObject _particle;
        private float _timer;
        private Transform _transform;
        private Rigidbody _rigidbody;

        private void Start()
        {
            _transform = transform;
            _rigidbody = _transform.GetComponent<Rigidbody>();
            _particle = Instantiate(_projectileParticle, _transform.position, _transform.rotation);
            _particle.transform.parent = _transform;

            if (_muzzleParticle is not null)
            {
                GameObject muzzle = Instantiate(_muzzleParticle, _transform.position, _transform.rotation);
                Destroy(muzzle, MuzzleDestroy);
            }
        }

        private void FixedUpdate()
        {
            Vector3 direction = _rigidbody.velocity;

            if (_rigidbody.velocity.sqrMagnitude != 0)
            {
                _transform.rotation = Quaternion.LookRotation(direction);
            }
            
            _timer += UnityEngine.Time.deltaTime;

            if (_timer > Time)
            {
                Destroy(_particle);
                Destroy(gameObject);
            }
        }
    }
}