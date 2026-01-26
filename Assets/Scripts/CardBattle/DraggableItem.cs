using Item;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CardBattle
{
    internal class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private ItemProvider _itemProvider;
        [SerializeField] private Image _imageToDrag;
        [SerializeField] private AspectRatioFitter _imageAspectRatioFitter;

        private PointerIndicator _indicator;
        private Canvas _canvas;
        private RectTransform _rectTransform;
        private Transform _defaultParent;
        private CanvasGroup _group;

        [Inject]
        private void Initialize(PointerIndicator indicator) =>
            _indicator = indicator;

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            _rectTransform = _imageToDrag.GetComponent<RectTransform>();
            _defaultParent = _rectTransform.parent;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _group = _imageToDrag.gameObject.GetComponent<CanvasGroup>();
            SwitchImagesAlfa(0.1f);
            _imageToDrag.raycastTarget = false;
            _imageAspectRatioFitter.enabled = false;
            _rectTransform.SetParent(_canvas.transform);
            _rectTransform.SetAsLastSibling();
            _indicator.BeginDrag();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
            _indicator.Drag();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            SwitchImagesAlfa(1f);
            _rectTransform.SetParent(_defaultParent);
            _rectTransform.anchoredPosition = Vector2.zero;
            _imageAspectRatioFitter.enabled = true;
            _imageToDrag.raycastTarget = true;
            
            RaycastResult raycast = eventData.pointerCurrentRaycast;
            _indicator.EndDrag();
            
            if (raycast.gameObject.TryGetComponent(out DropCardArea dropArea))
            {
                _itemProvider.Item.Select();
                dropArea.Receive();
            }
        }

        private void SwitchImagesAlfa(float number)
        {
            _group.alpha = number;
        }
    }
}