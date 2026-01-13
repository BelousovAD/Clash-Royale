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
        private Gameplay _gameplay;

        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _gameplay = new Gameplay(_endgameWindowId);

            _builder.AddSingleton(_gameplay);
            _builder.AddSingleton(new CrownCounter(CrownType.Enemy));
            _builder.AddSingleton(new CrownCounter(CrownType.Player));

            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Container container)
        {
            _builder.OnContainerBuilt -= Initialize;

            _gameplay.Initialize(
                container.Resolve<IEnumerable<CrownCounter>>(),
                container.Resolve<CoroutineTimer>(),
                container.Resolve<IWindowService>());
        }
    }
}