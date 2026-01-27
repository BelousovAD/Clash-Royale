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
        
        [SerializeField] private ItemProvider _itemProvider;
        [SerializeField] private Image _imageToDrag;
        [SerializeField] private AspectRatioFitter _imageAspectRatioFitter;

        private SpawnPointIndicator.SpawnPointIndicator _indicator;
        private Canvas _canvas;
        private RectTransform _rectTransform;
        private Transform _defaultParent;
        private CanvasGroup _group;

        [Inject]
        private void Initialize(SpawnPointIndicator.SpawnPointIndicator indicator) =>
            _indicator = indicator;

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
            _indicator.BeginDrag();
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _rectTransform.parent as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 mousePos);
            
            _rectTransform.anchoredPosition = mousePos;
            _indicator.Drag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _group.alpha = MaxAlpha;
            _rectTransform.SetParent(_defaultParent);
            _rectTransform.anchoredPosition = Vector2.zero;
            _imageAspectRatioFitter.enabled = true;
            _imageToDrag.raycastTarget = true;
            
            RaycastResult raycast = eventData.pointerCurrentRaycast;
            _indicator.EndDrag();
            
            if (raycast.gameObject is not null
                && raycast.gameObject.TryGetComponent(out DropCardArea dropArea))
            {
                _itemProvider.Item.Select();
                dropArea.Receive();
            }
        }
    }
}