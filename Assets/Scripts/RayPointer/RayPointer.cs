using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RayPointer
{
    public class RayPointer
    {
        private readonly LayerMask _layerMask;
        private readonly SpawnIndicator _spawnIndicator;
        private readonly Camera _camera;

        private RawImage _rawImage;

        public RayPointer(LayerMask layerMask, SpawnIndicator indicator, Camera camera)
        {
            _layerMask = layerMask;
            _spawnIndicator = indicator;
            _camera = camera;
        }

        public event Action DragEnded;
        
        public Ray Ray { get; private set; }

        public void Initialize(RawImage image) =>
            _rawImage = image;

        public void Drag(PointerEventData eventData)
        {
            Ray = GetRayFromUI(eventData);

            if (Physics.Raycast(Ray, out RaycastHit hitInfo, Mathf.Infinity, _layerMask)
                && hitInfo.collider.TryGetComponent(out Ground _))
            {
                _spawnIndicator.MoveIndicator(hitInfo.point);
            }
        }

        public void EndDrag()
        {
            _spawnIndicator.TurnOffIndicator();
            DragEnded?.Invoke();
        }

        private Ray GetRayFromUI(PointerEventData eventData)
        {
            RectTransform rectTransform = _rawImage.rectTransform;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform,
                eventData.position,
                null,
                out Vector2 localPoint);

            Rect rect = rectTransform.rect;

            float normalizedX = (localPoint.x - rect.x) / rect.width;
            float normalizedY = (localPoint.y - rect.y) / rect.height;

            return _camera.ViewportPointToRay(new Vector3(normalizedX, normalizedY, 0));
        }
    }
}