namespace Window
{
    using Common;
    using Reflex.Attributes;

    internal class CloseWindowButton : AbstractButton
    {
        private IWindowService _windowService;

        [Inject]
        private void Initialize(IWindowService windowService) =>
            _windowService = windowService;

        protected override void HandleClick() =>
            _windowService.CloseCurrent();
    }
}