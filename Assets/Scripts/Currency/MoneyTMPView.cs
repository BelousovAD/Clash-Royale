using Reflex.Attributes;
using Savvy.Extensions;
using TMPro;
using UnityEngine;

namespace Currency
{
    [RequireComponent(typeof(TMP_Text))]
    public class MoneyTMPView : MonoBehaviour
    {
        [SerializeField] private string _format = "{0}";

        private TMP_Text _textField;
        private Money _money;

        [Inject]
        private void Initialize(Money money) =>
            _money = money;

        private void Awake() =>
            _textField = GetComponent<TMP_Text>();

        private void OnEnable()
        {
            _money.Changed += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _money.Changed -= UpdateView;

        private void UpdateView() =>
            _textField.text = string.Format(_format, _money.Value.ToNumsFormat());
    }
}