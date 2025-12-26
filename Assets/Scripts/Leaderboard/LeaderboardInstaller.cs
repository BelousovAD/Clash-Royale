using Currency;
using Reflex.Core;
using UnityEngine;

namespace Leaderboard
{
    internal class LeaderboardInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private string _id;

        private ContainerBuilder _builder;
        private Leaderboard _leaderboard;

        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _leaderboard = new Leaderboard(_id);

            _builder.AddSingleton(_leaderboard);

            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Container container)
        {
            _builder.OnContainerBuilt -= Initialize;

            _leaderboard.Initialize(container.Resolve<Money>());
        }
    }
}