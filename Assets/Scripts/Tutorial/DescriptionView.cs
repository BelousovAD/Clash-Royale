namespace Tutorial
{
    using Bootstrap;
    using Reflex.Attributes;
    using TMPro;
    using UnityEngine;

    [RequireComponent(typeof(TMP_Text))]
    public class DescriptionView : MonoBehaviour
    {
        [SerializeField] private string _format = "{0}";
        [SerializeField] private Tutorial _tutorial;

        private TMP_Text _textField;
        private SavvyServicesProvider _services;

        [Inject]
        public void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;

        private void Awake() =>
            _textField = GetComponent<TMP_Text>();

        private void Start() =>
            _textField.text = string.Format(_format, _services.Localisation.GetTranslation(_tutorial.DescriptionKey));
    }
}