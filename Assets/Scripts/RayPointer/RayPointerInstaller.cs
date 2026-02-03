using Reflex.Core;
using UnityEngine;

namespace RayPointer
{
    internal class RayPointerInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private SpawnIndicator _indicator;

        public void InstallBindings(ContainerBuilder builder) =>
            builder.AddSingleton(new RayPointer(_layerMask,  _indicator, _camera));
    }
}