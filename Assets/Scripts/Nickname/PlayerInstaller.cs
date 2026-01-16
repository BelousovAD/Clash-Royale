using Bootstrap;
using Reflex.Core;
using UnityEngine;

namespace Nickname
{
    internal class PlayerInstaller : MonoBehaviour, IInstaller
    {
        private Player _player;
        private ContainerBuilder _builder;

        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _player = new Player();

            _builder.AddSingleton(_player, typeof(Opponent));

            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Container container)
        {
            _builder.OnContainerBuilt -= Initialize;

            _player.Initialize(container.Resolve<SavvyServicesProvider>());
        }
    }
}