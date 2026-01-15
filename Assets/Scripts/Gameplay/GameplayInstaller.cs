using System.Collections.Generic;
using Reflex.Core;
using Timer;
using UnityEngine;
using Window;

namespace Gameplay
{
    internal class GameplayInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private string _endgameWindowId;

        private ContainerBuilder _builder;
        private Judge _judge;

        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _judge = new Judge(_endgameWindowId);

            _builder.AddSingleton(_judge);
            _builder.AddSingleton(new CrownCounter(CrownType.Enemy));
            _builder.AddSingleton(new CrownCounter(CrownType.Player));

            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Container container)
        {
            _builder.OnContainerBuilt -= Initialize;

            _judge.Initialize(
                container.Resolve<IEnumerable<CrownCounter>>(),
                container.Resolve<CoroutineTimer>(),
                container.Resolve<IWindowService>());
        }
    }
}