using UnityEngine;
using UnityEngine.UI;

namespace Leaderboard
{
    internal class BackgroundView : MonoBehaviour
    {
        [SerializeField] private LeaderboardItem _item;
        [SerializeField] private Image _topPlayer;
        [SerializeField] private Image _ordinaryPlayer;
        [SerializeField] private Image _currentPlayer;

        private void OnEnable()
        {
            _item.Initialized += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _item.Initialized -= UpdateView;

        private void UpdateView()
        {
            _topPlayer.enabled = _item.IsInTop;
            _ordinaryPlayer.enabled = _item.IsInTop == false;
            _currentPlayer.enabled = _item.IsCurrentPlayer;
        }
    }
}