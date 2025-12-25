namespace Bootstrap
{
    using Reflex.Core;
    using UnityEngine;

    internal class BootstrapInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder) =>
            builder.AddSingleton(new SavvyServicesProvider());
    }
}