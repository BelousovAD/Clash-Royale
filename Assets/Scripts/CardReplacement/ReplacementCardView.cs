using System.Collections.Generic;
using DG.Tweening;
using Item;
using Reflex.Attributes;
using UnityEngine;

namespace CardReplacement
{
    [RequireComponent(typeof(CanvasGroup))]
    internal class ReplacementCardView : MonoBehaviour
    {
        private const float Duration = 0.5f;
        private const float AnimationAngle = 4f;
        private const int InfiniteLoops = -1;
        
        [SerializeField] private ContainerType _containerTypeToObserve;
        
        private CanvasGroup _canvasGroup;
        private Container _container;
        private Sequence _sequence;
        private RectTransform _rectTransform;
        private Tweener _tweener;

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

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            _container.SelectChanged += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _container.SelectChanged -= UpdateView;

        private void UpdateView()
        {
            if (_container.Selected is not null && _sequence == null)
            {
                _sequence = DOTween.Sequence();

                _sequence.Append(_rectTransform.DORotate(new Vector3(0, 0, AnimationAngle), Duration));
                _sequence.Append(_rectTransform.DORotate(new Vector3(0, 0, -AnimationAngle), Duration));
                _sequence.SetLoops(InfiniteLoops, LoopType.Yoyo);
                _sequence.SetEase(Ease.InOutSine);
            }
            else if (_container.Selected is null && _sequence != null)
            {
                _sequence.Kill();
                _tweener = _rectTransform.DORotate(new Vector3(0, 0, 0), Duration, RotateMode.Fast);
                _sequence = null;
            }
            
            _canvasGroup.interactable = _container.Selected is not null;
        }
    }
}