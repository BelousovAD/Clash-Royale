using Reflex.Attributes;

namespace Timer
{
    internal class InjectedTimerTMPView : TimerTMPView
    {
        [Inject]
        private new void Initialize(CoroutineTimer timer) =>
            base.Initialize(timer);
    }
}