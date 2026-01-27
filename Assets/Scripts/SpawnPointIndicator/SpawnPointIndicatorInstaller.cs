using Reflex.Core;
using UnityEngine;

namespace SpawnPointIndicator
{
    internal class SpawnPointIndicatorInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private Indicator _instance;
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _layerMask;

        public void InstallBindings(ContainerBuilder builder) =>
            builder.AddSingleton(new SpawnPointIndicator(_layerMask, _instance, _camera));
    }
}