using Reflex.Core;
using UnityEngine;

namespace RayPointer
{
    internal class RayPointerInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _layerMask;

        public void InstallBindings(ContainerBuilder builder) =>
            builder.AddSingleton(new RayPointer(_layerMask, _camera));
    }
}