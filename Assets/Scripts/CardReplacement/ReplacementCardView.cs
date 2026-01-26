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
        [SerializeField] private ContainerType _containerTypeToObserve;
        
        private CanvasGroup _canvasGroup;
        private Container _container;
        private Sequence _sequence;
        private float _duration = 0.5f;
        private float _animationAngle = 4f;
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

                _sequence.Append(_rectTransform.DORotate(new Vector3(0, 0, _animationAngle), _duration));
                _sequence.Append(_rectTransform.DORotate(new Vector3(0, 0, -_animationAngle), _duration));
                _sequence.SetLoops(-1, LoopType.Yoyo);
                _sequence.SetEase(Ease.InOutSine);
            }
            else if (_container.Selected is null && _sequence != null)
            {
                _sequence.Kill();
                _tweener = _rectTransform.DORotate(new Vector3(0, 0, 0), _duration, RotateMode.Fast);
                _sequence = null;
            }
            
            _canvasGroup.interactable = _container.Selected is not null;
        }
    }
}