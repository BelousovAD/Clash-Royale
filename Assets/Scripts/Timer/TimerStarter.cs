using Reflex.Attributes;
using UnityEngine;

namespace Timer
{
    internal class TimerStarter : MonoBehaviour
    {
        [SerializeField][Min(1)] private int _startTime = 90;

        private CoroutineTimer _timer;

        [Inject]
        private void Initialize(CoroutineTimer timer) =>
            _timer = timer;

        private void Start() =>
            _timer.Add(_startTime);
    }
}