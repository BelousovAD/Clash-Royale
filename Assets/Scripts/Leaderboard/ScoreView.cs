namespace Leaderboard
{
    using TMPro;
    using UnityEngine;

    [RequireComponent(typeof(TMP_Text))]
    internal class ScoreView : MonoBehaviour
    {
        [SerializeField] private LeaderboardItem _item;
        
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
            _textField.text = _item.Score.ToString();
    }
}