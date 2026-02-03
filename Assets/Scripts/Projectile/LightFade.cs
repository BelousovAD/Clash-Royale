using UnityEngine;

namespace Projectile
{
    public class LightFade : MonoBehaviour
    {
        [Header("Seconds to dim the light")]
        [SerializeField] private float _lifeTime = 0.2f;
        [SerializeField] private bool _killAfterLife = true;

        private Light _light;
        private float _intensity;

        private void Start()
        {
            if (gameObject.TryGetComponent<Light>(out Light _))
            {
                _light = gameObject.GetComponent<Light>();
                _intensity = _light.intensity;
            }
        }

        private void Update()
        {
            if (gameObject.TryGetComponent<Light>(out Light _))
            {
                _light.intensity -= _intensity * (Time.deltaTime / _lifeTime);

                if (_killAfterLife && _light.intensity <= 0)
                {
                    Destroy(gameObject.GetComponent<Light>());
                }
            }
        }
    }
}