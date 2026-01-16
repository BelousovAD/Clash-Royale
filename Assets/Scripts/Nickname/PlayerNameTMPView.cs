using Bootstrap;
using Reflex.Attributes;
using TMPro;
using UnityEngine;

namespace Nickname
{
    [RequireComponent(typeof(TMP_Text))]
    internal class PlayerNameTMPView : MonoBehaviour
    {
        [SerializeField] private string _format = "{0}";
        [SerializeField] private string _defaultNameLocalizationKey = "Player";
        
        private TMP_Text _textField;
        private Player _player;
        private SavvyServicesProvider _services;

        [Inject]
        private void Initialize(Player player, SavvyServicesProvider servicesProvider)
        {
            _player = player;
            _services = servicesProvider;
        }

        private void Awake() =>
            _textField = GetComponent<TMP_Text>();

        private void OnEnable() =>
            UpdateView();

        private void UpdateView()
        {
            string playerName = _player.IsLoggedIn
                ? _player.DisplayName
                : _services.Localisation.GetTranslation(_defaultNameLocalizationKey);
            _textField.text = string.Format(_format, playerName);
        }
    }
}