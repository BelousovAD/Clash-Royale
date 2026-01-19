using Item;
using Timer;
using UnityEngine;

namespace Chest
{
    internal class ChestTimerViewInitializer : ItemView
    {
        [SerializeField] private TimerTMPView _timerTMPView;
        
        private new Chest Item => base.Item as Chest;
        
        public override void UpdateView() =>
            _timerTMPView.Initialize(Item.Timer);
    }
}