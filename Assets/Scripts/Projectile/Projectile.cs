using UnityEngine;

namespace Projectile
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private GameObject _impactParticle;
        [SerializeField] private GameObject _projectileParticle;
        [SerializeField] private GameObject _muzzleParticle;

        [Header("Adjust if not using Sphere Collider")]
        [SerializeField] private float _colliderRadius = 1f;
        [SerializeField][Range(0f, 1f)] private float _collideOffset = 0.15f;

        private float _timer;
        private float _time = 4;
        private Transform _transform;
        private Rigidbody _rigidbody;

        void Start()
        {
            _transform = transform;
            _rigidbody = _transform.GetComponent<Rigidbody>();
            _projectileParticle = Instantiate(_projectileParticle, _transform.position, _transform.rotation);
            _projectileParticle.transform.parent = _transform;

            if (_muzzleParticle)
            {
                _muzzleParticle = Instantiate(_muzzleParticle, _transform.position, _transform.rotation);
                Destroy(_muzzleParticle, 1.5f);
            }
        }

        void FixedUpdate()
        {
            if (_rigidbody.velocity.magnitude != 0)
            {
                _transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            }

            RaycastHit hit;

            float radius;

            if (_transform.TryGetComponent<SphereCollider>(out SphereCollider sphereCollider))
            {
                radius = sphereCollider.radius;
            }
            else
            {
                radius = _colliderRadius;
            }

            Vector3 direction = _rigidbody.velocity;

            if (_rigidbody.useGravity)
            {
                direction += Physics.gravity * Time.deltaTime;
            }

            direction = direction.normalized;

            float detectionDistance = _rigidbody.velocity.magnitude * Time.deltaTime;

            if (Physics.SphereCast(_transform.position, radius, direction, out hit, detectionDistance))
            {
                _transform.position = hit.point + (hit.normal * _collideOffset);

                GameObject impactP = Instantiate(_impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, hit.normal));

                ParticleSystem[] trails = GetComponentsInChildren<ParticleSystem>();

                for (int i = 1; i < trails.Length; i++)
                {
                    ParticleSystem trail = trails[i];

                    if (trail.gameObject.name.Contains("Trail"))
                    {
                        trail.transform.SetParent(null);
                        trail.transform.SetParent(null);
                        Destroy(trail.gameObject, 2f);
                    }
                }

                Destroy(_projectileParticle, 3f);
                Destroy(impactP, 3.5f);
                Destroy(gameObject);
            }
            else
            {
                _timer += Time.deltaTime;

                if (Mathf.Approximately(_timer, _time))
                {
                    Destroy(_projectileParticle);
                    Destroy(gameObject);
                }
            }
        }
    }
}