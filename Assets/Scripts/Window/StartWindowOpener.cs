using Reflex.Attributes;
using UnityEngine;

namespace Window
{
    internal class StartWindowOpener : MonoBehaviour
    {
        private const int CountToClose = 0;
        
        [SerializeField] private string _windowId;

        private IWindowService _windowService;

        [Inject]
        private void Initialize(IWindowService windowService) =>
            _windowService = windowService;

        private void Start() =>
            _windowService.Open(_windowId, CountToClose);
    }
}