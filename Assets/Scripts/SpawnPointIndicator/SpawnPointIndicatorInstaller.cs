using Reflex.Core;
using UnityEngine;

namespace SpawnPointIndicator
{
    internal class SpawnPointIndicatorInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private SpawnIndicator _indicator;
        [SerializeField] private AreaIdentifier _areaIdentifier;

        public void InstallBindings(ContainerBuilder builder) =>
            builder.AddSingleton(new RayPointer(_layerMask,  _indicator, _camera, _areaIdentifier));
    }
}