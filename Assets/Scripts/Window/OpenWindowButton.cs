namespace Window
{
    using Common;
    using Reflex.Attributes;
    using UnityEngine;

    internal class OpenWindowButton : AbstractButton
    {
        [SerializeField] private string _windowId;
        [SerializeField] private bool _needCloseCurrent;
        
        private IWindowService _windowService;

        [Inject]
        private void Initialize(IWindowService windowService) =>
            _windowService = windowService;

        protected override void HandleClick() =>
            _windowService.Open(_windowId, _needCloseCurrent);
    }
}