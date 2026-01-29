using System;
using System.Collections.Generic;
using DG.Tweening;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    [RequireComponent(typeof(Image))]
    public class CrownIconView : MonoBehaviour
    {
        private const int InfiniteLoops = -1;
        private const float AnimationAngle = 4f;
        private const float Duration = 1f;

        [SerializeField] private Sprite _defaultIcon;
        [SerializeField] private Sprite _activeIcon;
        [SerializeField] private CrownType _type;
        [SerializeField][Min(1)] private int _number = 1;

        private Image _image;
        private CrownCounter _counter;
        private Sequence _sequence;
        private RectTransform _rectTransform;

        [Inject]
        private void Initialize(IEnumerable<CrownCounter> crownCounters)
        {
            foreach (CrownCounter counter in crownCounters)
            {
                if (counter.Type == _type)
                {
                    _counter = counter;
                    break;
                }
            }
        }

        private void Awake()
        {
            _image = GetComponent<Image>();
            _rectTransform = GetComponent<RectTransform>();
        }

        private void OnEnable() =>
            UpdateView();

        private void OnDisable() =>
            _sequence?.Kill();

        private void UpdateView()
        {
            _image.sprite = _counter.Count >= _number ? _activeIcon : _defaultIcon;

            _sequence = DOTween.Sequence();
            _sequence.SetUpdate(UpdateType.Normal, true);
            _sequence.SetEase(Ease.InOutQuad);
            _sequence.Append(_rectTransform.DORotate(new Vector3(0, 0, AnimationAngle), Duration));
            _sequence.Join(_rectTransform.DOScale(
                new Vector3(_rectTransform.localScale.x + 0.1f
                    , _rectTransform.localScale.y + 0.1f
                    , _rectTransform.localScale.z + 0.1f)
                , Duration));
            _sequence.SetLoops(InfiniteLoops, LoopType.Yoyo);
        }
    }
}