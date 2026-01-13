using TMPro;
using UnityEngine;
using Savvy.Extensions;

namespace Leaderboard
{
    [RequireComponent(typeof(TMP_Text))]
    internal class ScoreView : MonoBehaviour
    {
        [SerializeField] private LeaderboardItem _item;
        [SerializeField] private string _format = "{0}";

        private TMP_Text _textField;

        private void Awake() =>
            _textField = GetComponent<TMP_Text>();

        private void OnEnable()
        {
            _item.Initialized += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _item.Initialized -= UpdateView;

        private void UpdateView() =>
            _textField.text = string.Format(_format, _item.Score.ToNumsFormat());
    }
}