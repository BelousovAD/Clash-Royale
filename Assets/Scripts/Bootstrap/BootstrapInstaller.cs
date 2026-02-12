using Reflex.Core;
using UnityEngine;

namespace Bootstrap
{
    internal class BootstrapInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder) =>
            builder.AddSingleton(new SavvyServicesProvider());
    }
}