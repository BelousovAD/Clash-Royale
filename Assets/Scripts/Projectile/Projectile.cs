using UnityEngine;

namespace Projectile
{
    internal class Projectile : MonoBehaviour
    {
        private const float Time = 4;
        private const float MuzzleDestroy = 1.5f;
        private const float ProjectileDestroy = 3f;
        private const float ImpactDestroy = 3.5f;

        [SerializeField] private GameObject _impactParticle;
        [SerializeField] private GameObject _projectileParticle;
        [SerializeField] private GameObject _muzzleParticle;

        [Header("Adjust if not using Sphere Collider")]
        [SerializeField] private float _colliderRadius = 1f;
        [SerializeField][Range(0f, 1f)] private float _collideOffset = 0.15f;

        private GameObject _impact;
        private GameObject _projectile;
        private float _timer;
        private Transform _transform;
        private Rigidbody _rigidbody;

        private void Start()
        {
            _transform = transform;
            _rigidbody = _transform.GetComponent<Rigidbody>();
            _projectile = Instantiate(_projectileParticle, _transform.position, _transform.rotation);
            _projectile.transform.parent = _transform;

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

            RaycastHit hit;
            float radius;

            if (_transform.TryGetComponent(out SphereCollider sphereCollider))
            {
                radius = sphereCollider.radius;
            }
            else
            {
                radius = _colliderRadius;
            }

            if (_rigidbody.useGravity)
            {
                direction += Physics.gravity * UnityEngine.Time.deltaTime;
            }

            direction = direction.normalized;

            float detectionDistance = _rigidbody.velocity.magnitude * UnityEngine.Time.deltaTime;

            if (Physics.SphereCast(_transform.position, radius, direction, out hit, detectionDistance))
            {
                _transform.position = hit.point + (hit.normal * _collideOffset);

                _impact = Instantiate(_impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, hit.normal));

                Destroy(_projectile, ProjectileDestroy);
                Destroy(_impact, ImpactDestroy);
                Destroy(gameObject);
            }
            else
            {
                _timer += UnityEngine.Time.deltaTime;

                if (Mathf.Approximately(_timer, Time))
                {
                    Destroy(_projectile);
                    Destroy(gameObject);
                }
            }
        }
    }
}