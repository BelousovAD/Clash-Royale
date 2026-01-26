using Item;
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
        [SerializeField] private PointerIndicator _indicator;

        private Canvas _canvas;
        private RectTransform _rectTransform;
        private Transform _defaultParent;
        private CanvasGroup _group;

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            _rectTransform = _imageToDrag.GetComponent<RectTransform>();
            _defaultParent = _rectTransform.parent;
            _indicator = FindAnyObjectByType<PointerIndicator>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _group = _imageToDrag.gameObject.GetComponent<CanvasGroup>();
            SwitchImagesAlfa(0.1f);
            _imageToDrag.raycastTarget = false;
            _imageAspectRatioFitter.enabled = false;
            _rectTransform.SetParent(_canvas.transform);
            _rectTransform.SetAsLastSibling();
            _indicator.OnBeginDrag();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
            _indicator.OnDrag();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            SwitchImagesAlfa(1f);
            _rectTransform.SetParent(_defaultParent);
            _rectTransform.anchoredPosition = Vector2.zero;
            _imageAspectRatioFitter.enabled = true;
            _imageToDrag.raycastTarget = true;
            
            RaycastResult raycast = eventData.pointerCurrentRaycast;
            _indicator.OnEndDrag();
            
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