using System;
using System.Collections;
using System.Collections.Generic;
using Reflex.Attributes;
using UnityEngine;
using Window;

namespace Tutorial
{
    internal class TutorialOpener : MonoBehaviour
    {
        private const int CountToClose = 0;
        
        [SerializeField] private float _delay = 0.1f;
        [SerializeField] private List<StageWindow> _stages;
        
        private Tutorial _tutorial;
        private IWindowService _windowService;
        private WaitForSeconds _wait;

        [Inject]
        private void Initialize(Tutorial tutorial, IWindowService windowService)
        {
            _tutorial = tutorial;
            _windowService = windowService;
        }

        private void Awake() =>
            _wait = new WaitForSeconds(_delay);

        private void OnEnable()
        {
            if (_tutorial.IsCompleted)
            {
                return;
            }
            
            foreach (StageWindow stageWindow in _stages)
            {
                if (stageWindow.Number > _tutorial.LastStage)
                {
                    StartCoroutine(OpenTutorialAfterDelay(stageWindow.WindowId));
                    break;
                }
            }
        }

        private IEnumerator OpenTutorialAfterDelay(string windowId)
        {
            yield return _wait;

            _windowService.Open(windowId, CountToClose);
        }
        
        [Serializable]
        private struct StageWindow
        {
            public int Number;
            public string WindowId;
        }
    }
}