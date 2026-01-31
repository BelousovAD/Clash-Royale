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
        private readonly AreaDetector _areaDetector;

        private RawImage _rawImage;
        private Ray _ray;

        public RayPointer(LayerMask layerMask, SpawnIndicator indicator, Camera camera, AreaDetector areaDetector)
        {
            _layerMask = layerMask;
            _spawnIndicator = indicator;
            _camera = camera;
            _areaDetector = areaDetector;
        }

        public void Initialize(RawImage image) =>
            _rawImage = image;

        public void Drag(PointerEventData eventData)
        {
            _ray = GetRayFromUI(eventData);

            if (Physics.Raycast(_ray, out RaycastHit hitInfo, Mathf.Infinity, _layerMask)
                && hitInfo.collider.TryGetComponent(out Ground _))
            {
                _spawnIndicator.MoveIndicator(hitInfo.point);
            }
        }

        public void EndDrag() =>
            _spawnIndicator.TurnOffIndicator();

        public void SearchArea() =>
            _areaDetector.FindDropCardArea(_ray);

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