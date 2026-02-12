using System.Collections.Generic;
using Reflex.Core;
using UnityEngine;
using Container = Item.Container;

namespace ArtificialOpponent
{
    internal class ArtificialOpponentInstaller : MonoBehaviour, IInstaller
    {
        private ContainerBuilder _builder;
        private CardChooser _cardChooser;

        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _cardChooser = new CardChooser();

            _builder.AddSingleton(_cardChooser);

            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Reflex.Core.Container container)
        {
            _builder.OnContainerBuilt -= Initialize;

            _cardChooser.Initialize(
                container.Resolve<IEnumerable<Container>>(),
                container.Resolve<IEnumerable<Currency.Currency>>());
        }
    }
}