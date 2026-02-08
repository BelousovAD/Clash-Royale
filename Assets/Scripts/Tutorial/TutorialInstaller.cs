using Bootstrap;
using Reflex.Core;
using UnityEngine;

namespace Tutorial
{
    internal class TutorialInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private string _saveKey = "Tutorial";
        [SerializeField][Min(1)] private int _maxStage = 1;
        
        private ContainerBuilder _builder;
        private Tutorial _tutorial;

        public void InstallBindings(ContainerBuilder builder)
        {
            _builder = builder;
            _tutorial = new Tutorial(_saveKey, _maxStage);

            _builder.AddSingleton(_tutorial);

            _builder.OnContainerBuilt += Initialize;
        }

        private void Initialize(Container container) =>
            _tutorial.Initialize(container.Resolve<SavvyServicesProvider>());
    }
}