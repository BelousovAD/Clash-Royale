using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UnitHealth
{
    [RequireComponent(typeof(Slider))]
    internal class HealthSmoothSliderView : HealthView
    {
        [SerializeField] private float _changingTime = 1f;

        private Slider _slider;
        private float _targetValue;
        private float _changingSpeed;
        private Coroutine _changingValueSmoothly;

        private void Awake() =>
            _slider = GetComponent<Slider>();

        protected override void UpdateView()
        {
            if (Health is not null)
            {
                _targetValue = Health.Value / Health.MaxValue;
                _changingSpeed = Mathf.Abs(_targetValue - _slider.value) / _changingTime;
                _changingValueSmoothly ??= StartCoroutine(ChangeValueSmoothly());
            }
            else if (_changingValueSmoothly is not null)
            {
                StopCoroutine(_changingValueSmoothly);
                _changingValueSmoothly = null;
            }
        }
        
        private IEnumerator ChangeValueSmoothly()
        {
            while (isActiveAndEnabled && Mathf.Approximately(_targetValue, _slider.value) == false)
            {
                _slider.value = Mathf.MoveTowards(_slider.value, _targetValue, Time.deltaTime * _changingSpeed);

                yield return null;
            }

            _slider.value = _targetValue;
            _changingValueSmoothly = null;
        }
    }
}