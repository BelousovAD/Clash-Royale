namespace Tutorial
{
    using System.Collections;
    using System.Collections.Generic;
    using Bootstrap;
    // using Level;
    using Reflex.Attributes;
    using UnityEngine;
    using Window;

    internal class TutorialOpener : MonoBehaviour
    {
        [SerializeField] private string _windowId;
        [SerializeField] private float _delay = 0.1f;
        [SerializeField] private List<TutorialData> _datas;

        // private Level _level;
        private SavvyServicesProvider _services;
        private IWindowService _windowService;
        private WaitForSeconds _wait;

        private void Awake() =>
            _wait = new WaitForSeconds(_delay);

        [Inject]
        private void Initialize(
            // Level level,
            SavvyServicesProvider servicesProvider,
            IWindowService windowService)
        {
            // _level = level;
            _services = servicesProvider;
            _windowService = windowService;
        }

        private void Start()
        {
            foreach (TutorialData data in _datas)
            {
                if (/*data.Level != _level.Chosen ||*/ _services.Preferences.LoadBool(data.SaveKey))
                {
                    continue;
                }
                
                StartCoroutine(OpenTutorialAfterDelay(data));
                _services.Preferences.SaveBool(data.SaveKey, true);
            }
        }

        private IEnumerator OpenTutorialAfterDelay(TutorialData data)
        {
            yield return _wait;
            
            _windowService.Open(_windowId, false).GetComponent<Tutorial>().Initialize(data);
        }
    }
}