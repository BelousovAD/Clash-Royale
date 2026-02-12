using UnityEngine;
using UnityEngine.UI;

namespace UnitHealth
{
    [RequireComponent(typeof(Slider))]
    internal class HealthSmoothSliderView : HealthView
    {
        [SerializeField] private float _changingTime = 1f;

        private Slider _slider;
        private float _targetValue = 1f;
        private float _changingSpeed;

        private void Awake() =>
            _slider = GetComponent<Slider>();

        private void Update()
        {
            if (Mathf.Approximately(_targetValue, _slider.value) == false)
            {
                _slider.value = Mathf.MoveTowards(_slider.value, _targetValue, Time.deltaTime * _changingSpeed);
            }
        }

        protected override void UpdateView()
        {
            if (Health is not null)
            {
                _targetValue = Health.Value / Health.MaxValue;
                _changingSpeed = Mathf.Abs(_targetValue - _slider.value) / _changingTime;
            }
        }
    }
}