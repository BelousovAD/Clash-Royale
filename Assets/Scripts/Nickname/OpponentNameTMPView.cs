using System.Collections.Generic;
using Reflex.Attributes;
using TMPro;
using UnityEngine;

namespace Nickname
{
    [RequireComponent(typeof(TMP_Text))]
    internal class OpponentNameTMPView : MonoBehaviour
    {
        [SerializeField] private string _format = "{0}";
        [SerializeField] private OpponentType _type;
        
        private TMP_Text _textField;
        private Opponent _opponent;

        [Inject]
        private void Initialize(IEnumerable<Opponent> opponents)
        {
            foreach (Opponent opponent in opponents)
            {
                if (opponent.Type == _type)
                {
                    _opponent = opponent;
                    break;
                }
            }
        }

        private void Awake() =>
            _textField = GetComponent<TMP_Text>();

        private void OnEnable()
        {
            _opponent.Renamed += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _opponent.Renamed -= UpdateView;

        private void UpdateView() =>
            _textField.text = string.Format(_format, _opponent.Name);
    }
}