using System.Collections.Generic;
using Bootstrap;
using Reflex.Attributes;
using UnityEngine;

namespace Item
{
    internal class ContainerLoader : MonoBehaviour, ILoadable
    {
        private List<Container> _containers;

        [Inject]
        private void Initialize(IEnumerable<Container> containers) =>
            _containers = new List<Container>(containers);

        public void Load() =>
            _containers.ForEach(container => container.Load());
    }
}