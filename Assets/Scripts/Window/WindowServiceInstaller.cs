using MirraGames.SDK;
using Reflex.Core;
using UnityEngine;

namespace Window
{
    internal class WindowServiceInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private MonoBehaviour _horizontalSpawner;
        [SerializeField] private MonoBehaviour _verticalSpawner;

        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(
                new WindowService((MirraSDK.Device.IsMobile ? _verticalSpawner : _horizontalSpawner) as IWindowSpawner),
                typeof(IWindowService));
        }

        private void OnValidate()
        {
            if (_horizontalSpawner is not null && _horizontalSpawner is not IWindowSpawner)
            {
                Debug.LogError($"{nameof(_horizontalSpawner)} must inherited {nameof(IWindowSpawner)}");
                _horizontalSpawner = null;
            }

            if (_verticalSpawner is not null && _verticalSpawner is not IWindowSpawner)
            {
                Debug.LogError($"{nameof(_verticalSpawner)} must inherited {nameof(IWindowSpawner)}");
                _verticalSpawner = null;
            }
        }
    }
}