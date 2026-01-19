using TMPro;
using UnityEngine;

namespace Timer
{
    [RequireComponent(typeof(TMP_Text))]
    public class TimerTMPView : MonoBehaviour
    {
        private const int Min2Sec = 60;

        [SerializeField] private string _format = "{0:D2}:{1:D2}";

        private TMP_Text _textField;
        private CoroutineTimer _timer;

        public void Initialize(CoroutineTimer timer)
        {
            Unsubscribe();
            _timer = timer;
            Subscribe();
            UpdateView();
        }

        private void Awake() =>
            _textField = GetComponent<TMP_Text>();

        private void OnEnable()
        {
            Subscribe();
            UpdateView();
        }

        private void OnDisable() =>
            Unsubscribe();

        private void Subscribe()
        {
            if (_timer is not null)
            {
                _timer.TimeChanged += UpdateView;
            }
        }

        private void Unsubscribe()
        {
            if (_timer is not null)
            {
                _timer.TimeChanged -= UpdateView;
            }
        }

        private void UpdateView()
        {
            int minutes = _timer.Time / Min2Sec;
            int seconds = _timer.Time % Min2Sec;
            _textField.text = string.Format(_format, minutes, seconds);
        }
    }
}