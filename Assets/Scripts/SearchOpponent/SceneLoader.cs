using Bootstrap;
using Reflex.Attributes;
using UnityEngine;

namespace SearchOpponent
{
    internal class SceneLoader : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        private SavvyServicesProvider _services;

        [Inject]
        private void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;

        public void Load() =>
            _services.SceneLoader.LoadAdditiveSceneAsync(_sceneName);
    }
}