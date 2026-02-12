using Bootstrap;
using Reflex.Attributes;
using TMPro;
using UnityEngine;

namespace Localization
{
    [RequireComponent(typeof(TMP_Text))]
    internal class LocalizedFormatedText : MonoBehaviour
    {
        [SerializeField] private string _localizationKey = "Max";
        [SerializeField] private string _format = "{0}";
        
        private TMP_Text _textField;
        private SavvyServicesProvider _services;

        [Inject]
        private void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;
        
        private void Awake() =>
            _textField = GetComponent<TMP_Text>();
        
        private void OnEnable()
        {
            _services.Localisation.LocalizationUpdated += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _services.Localisation.LocalizationUpdated -= UpdateView;

        private void UpdateView() =>
            _textField.text = string.Format(_format, _services.Localisation.GetTranslation(_localizationKey));
    }
}
