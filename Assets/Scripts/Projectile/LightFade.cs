using UnityEngine;

namespace Projectile
{
    internal class LightFade : MonoBehaviour
    {
        [Header("Seconds to dim the light")]
        [SerializeField] private float _lifeTime = 0.2f;
        [SerializeField] private bool _killAfterLife = true;

        private Light _light;
        private float _intensity;

        private void Start()
        {
            if (gameObject.TryGetComponent(out Light light))
            {
                _light = light;
                _intensity = _light.intensity;
            }
        }

        private void Update()
        {
            _light.intensity -= _intensity * (Time.deltaTime / _lifeTime);

            if (_killAfterLife && _light.intensity <= 0)
            {
                Destroy(_light);
            }
        }
    }
}