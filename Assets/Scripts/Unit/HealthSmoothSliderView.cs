using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Unit
{
    [RequireComponent(typeof(Slider))]
    internal class HealthSmoothSliderView : MonoBehaviour
    {
        [SerializeField] private Unit _unit;
        [SerializeField] private float _changingTime = 1f;

        private Slider _slider;
        private Health _health;
        private float _targetValue;
        private float _changingSpeed;
        private Coroutine _changingValueSmoothly;

        private void Awake() =>
            _slider = GetComponent<Slider>();

        private void OnEnable()
        {
            _unit.Initialized += UpdateSubscriptions;
            UpdateSubscriptions();
        }

        private void OnDisable()
        {
            _unit.Initialized -= UpdateSubscriptions;
            Unsubscribe();
        }

        private void Subscribe() =>
            _health.Changed += UpdateView;

        private void Unsubscribe() =>
            _health.Changed -= UpdateView;

        private void UpdateSubscriptions()
        {
            if (_health is not null)
            {
                Unsubscribe();
            }

            _health = _unit.Health;

            if (_health is not null)
            {
                Subscribe();
            }

            UpdateView();
        }

        private void UpdateView()
        {
            if (_health is not null)
            {
                _targetValue = _health.Value / _health.MaxValue;
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