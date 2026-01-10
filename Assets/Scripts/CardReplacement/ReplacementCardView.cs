using System.Collections.Generic;
using Item;
using Reflex.Attributes;
using UnityEngine;

namespace CardReplacement
{
    [RequireComponent(typeof(CanvasGroup))]
    internal class ReplacementCardView : MonoBehaviour
    {
        [SerializeField] private ContainerType _containerTypeToObserve;
        
        private CanvasGroup _canvasGroup;
        private Container _container;

        [Inject]
        private void Initialize(IEnumerable<Container> containers)
        {
            foreach (Container container in containers)
            {
                if (container.Type == _containerTypeToObserve)
                {
                    _container = container;
                    break;
                }
            }
        }

        private void Awake() =>
            _canvasGroup = GetComponent<CanvasGroup>();

        private void OnEnable()
        {
            _container.SelectChanged += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _container.SelectChanged -= UpdateView;

        private void UpdateView() =>
            _canvasGroup.interactable = _container.Selected is not null;
    }
}