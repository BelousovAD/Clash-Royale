using Reflex.Attributes;
using UnityEngine;

namespace Gameplay
{
    internal class VictoryStatusView : MonoBehaviour
    {
        [SerializeField] private bool _isPlayer;
        
        private Judge _judge;

        [Inject]
        private void Initialize(Judge judge)
        {
            _judge = judge;
            _judge.VictoryStatusChanged += UpdateView;
            UpdateView();
        }

        private void OnDestroy() =>
            _judge.VictoryStatusChanged -= UpdateView;

        private void UpdateView() =>
            gameObject.SetActive((_judge.IsVictory ^ _isPlayer) == false);
    }
}