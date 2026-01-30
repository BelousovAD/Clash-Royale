using TMPro;
using UnityEngine;

namespace Unit
{
    [RequireComponent(typeof(TMP_Text))]
    internal class HealthTextView : HealthView
    {
        [SerializeField] private string _format = "{0}";
        
        private TMP_Text _textField;

        private void Awake() =>
            _textField = GetComponent<TMP_Text>();

        protected override void UpdateView() =>
            _textField.text = string.Format(_format, Health is null ? string.Empty : Health.Value);
    }
}