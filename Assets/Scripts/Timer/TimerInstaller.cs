using Bootstrap;
using Reflex.Core;
using UnityEngine;

namespace Timer
{
    internal class TimerInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField][Min(1)] private int _maxTime = 90;

        private CoroutineTimer _timer;
        private ContainerBuilder _builder;

        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _timer = new CoroutineTimer(_maxTime);

            _builder.AddSingleton(_timer);

            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Container container)
        {
            _builder.OnContainerBuilt -= Initialize;

            _timer.Initialize(container.Resolve<SavvyServicesProvider>());
        }
    }
}