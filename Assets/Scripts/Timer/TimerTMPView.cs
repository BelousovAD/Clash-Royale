using Reflex.Attributes;
using TMPro;
using UnityEngine;

namespace Timer
{
    [RequireComponent(typeof(TMP_Text))]
    internal class TimerTMPView : MonoBehaviour
    {
        private const int Min2Sec = 60;

        [SerializeField] private string _format = "{0:D2}:{1:D2}";

        private TMP_Text _textField;
        private CoroutineTimer _timer;

        [Inject]
        private void Initialize(CoroutineTimer timer) =>
            _timer = timer;

        private void Awake() =>
            _textField = GetComponent<TMP_Text>();

        private void OnEnable()
        {
            _timer.TimeChanged += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _timer.TimeChanged -= UpdateView;

        private void UpdateView()
        {
            int minutes = _timer.Time / Min2Sec;
            int seconds = _timer.Time % Min2Sec;
            _textField.text = string.Format(_format, minutes, seconds);
        }
    }
}