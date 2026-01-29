using System.Collections.Generic;
using System.Linq;
using Audio;
using Item;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CardBattle
{
    internal class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private const float MinAlpha = 0.1f;
        private const float MaxAlpha = 1f;
        private const Audio.AudioType SoundType = Audio.AudioType.Sound;

        [SerializeField] private ItemProvider _itemProvider;
        [SerializeField] private Image _imageToDrag;
        [SerializeField] private AspectRatioFitter _imageAspectRatioFitter;
        [SerializeField] private AudioClipKey _key = AudioClipKey.CardClick;

        private RayPointer.RayPointer _rayPointer;
        private Canvas _canvas;
        private RectTransform _rectTransform;
        private Transform _defaultParent;
        private CanvasGroup _group;
        private Audio.Audio _audio;

        [Inject]
        private void Initialize(RayPointer.RayPointer rayPointer, IEnumerable<Audio.Audio> audios)
        {
            _rayPointer = rayPointer;
            _audio = audios.FirstOrDefault(audioObject => audioObject.Type == SoundType);
        }

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            _rectTransform = _imageToDrag.GetComponent<RectTransform>();
            _group = _imageToDrag.GetComponent<CanvasGroup>();
            _defaultParent = _rectTransform.parent;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _group.alpha = MinAlpha;
            _imageToDrag.raycastTarget = false;
            _imageAspectRatioFitter.enabled = false;
            _rectTransform.SetParent(_canvas.transform);
            _rectTransform.SetAsLastSibling();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _rectTransform.parent as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 pointerPosition);
            _rectTransform.anchoredPosition = pointerPosition;
            _itemProvider.Item.Select();
            _audio.Play(_key);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
            _rayPointer.Drag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _rayPointer.EndDrag();
            _group.alpha = MaxAlpha;
            _rectTransform.SetParent(_defaultParent);
            _rectTransform.anchoredPosition = Vector2.zero;
            _imageAspectRatioFitter.enabled = true;
            _imageToDrag.raycastTarget = true;
            _rayPointer.SearchArea();
        }
    }
}