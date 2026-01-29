using Item;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CardBattle
{
    [RequireComponent(typeof(AudioSource))]
    internal class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private const float MinAlpha = 0.1f;
        private const float MaxAlpha = 1f;

        [SerializeField] private ItemProvider _itemProvider;
        [SerializeField] private Image _imageToDrag;
        [SerializeField] private AspectRatioFitter _imageAspectRatioFitter;

        private RayPointer.RayPointer _rayPointer;
        private Canvas _canvas;
        private RectTransform _rectTransform;
        private Transform _defaultParent;
        private CanvasGroup _group;
        private AudioSource _source;

        [Inject]
        private void Initialize(RayPointer.RayPointer rayPointer) =>
            _rayPointer = rayPointer;

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            _rectTransform = _imageToDrag.GetComponent<RectTransform>();
            _group = _imageToDrag.GetComponent<CanvasGroup>();
            _source = GetComponent<AudioSource>();
            _defaultParent = _rectTransform.parent;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _source.Play();
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