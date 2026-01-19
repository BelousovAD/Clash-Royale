using Item;
using Timer;
using UnityEngine;

namespace Chest
{
    internal class ChestTimerViewInitializer : ItemView<Chest>
    {
        [SerializeField] private TimerTMPView _timerTMPView;

        protected override void UpdateView() =>
            _timerTMPView.Initialize(Item?.Timer);
    }
}