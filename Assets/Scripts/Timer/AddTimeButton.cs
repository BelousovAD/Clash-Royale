using Common;
using Reflex.Attributes;
using UnityEngine;

namespace Timer
{
    internal class AddTimeButton : AbstractButton
    {
        [SerializeField][Min(1)] private int _amount;

        private CoroutineTimer _timer;

        [Inject]
        private void Initialize(CoroutineTimer timer) =>
            _timer = timer;

        protected override void HandleClick() =>
            _timer.Add(_amount);
    }
}