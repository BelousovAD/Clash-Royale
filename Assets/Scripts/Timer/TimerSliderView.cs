using System.Collections;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Timer
{
    [RequireComponent(typeof(Slider))]
    public class TimerSliderView : MonoBehaviour
    {
        [SerializeField][Min(0)] private float _changingTime = 1;

        private Slider _slider;
        private CoroutineTimer _timer;
        private float _targetValue = 1;
        private float _changingSpeed;
        private Coroutine _changingValueSmoothly;

        [Inject]
        private void Initialize(CoroutineTimer timer) =>
            _timer = timer;

        private void Awake() =>
            _slider = GetComponent<Slider>();

        private void OnEnable()
        {
            _slider.value = _targetValue;
            _timer.TimeChanged += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _timer.TimeChanged -= UpdateView;

        private void UpdateView()
        {
            _targetValue = (float)_timer.Time / _timer.Max;
            _changingSpeed = Mathf.Abs(_targetValue - _slider.value) / _changingTime;
            _changingValueSmoothly ??= StartCoroutine(ChangeValueSmoothly());
        }

        private IEnumerator ChangeValueSmoothly()
        {
            while (isActiveAndEnabled && IsTargetReached() == false)
            {
                _slider.value = Mathf.MoveTowards(_slider.value, _targetValue, Time.deltaTime * _changingSpeed);

                yield return null;
            }

            _slider.value = _targetValue;
            _changingValueSmoothly = null;
        }

        private bool IsTargetReached() =>
            Mathf.Approximately(_targetValue, _slider.value);
    }
}