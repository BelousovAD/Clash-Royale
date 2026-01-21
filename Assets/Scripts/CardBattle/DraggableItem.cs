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

        private Canvas _canvas;
        private RectTransform _rectTransform;
        private Transform _defaultParent;

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            _rectTransform = _imageToDrag.GetComponent<RectTransform>();
            _defaultParent = _rectTransform.parent;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _imageToDrag.raycastTarget = false;
            _imageAspectRatioFitter.enabled = false;
            _rectTransform.SetParent(_canvas.transform);
            _rectTransform.SetAsLastSibling();
            _itemProvider.Item.Select();
        }

        public void OnDrag(PointerEventData eventData) =>
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;

        public void OnEndDrag(PointerEventData eventData)
        {
            _rectTransform.SetParent(_defaultParent);
            _rectTransform.anchoredPosition = Vector2.zero;
            _imageAspectRatioFitter.enabled = true;
            _imageToDrag.raycastTarget = true;
            
            RaycastResult raycast = eventData.pointerCurrentRaycast;
            
            if (raycast.gameObject.TryGetComponent(out DropItemArea dropArea))
            {
                dropArea.Receive();
            }
        }
    }
}