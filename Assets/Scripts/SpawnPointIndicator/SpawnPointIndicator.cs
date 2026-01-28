using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SpawnPointIndicator
{
    public class SpawnPointIndicator
    {
        private static readonly Vector3 DefaultPosition = new (0, -5, 0);

        private readonly LayerMask _layerMask;
        private readonly Indicator _indicatorInstance;
        private readonly Camera _camera;

        private RawImage _rawImage;
        private Ray _ray;

        public SpawnPointIndicator(LayerMask layerMask, Indicator indicator, Camera camera)
        {
            _layerMask = layerMask;
            _indicatorInstance = indicator;
            _camera = camera;
        }

        public void Initialize(RawImage image) =>
            _rawImage = image;

        public void BeginDrag() =>
            _indicatorInstance.gameObject.SetActive(true);

        public void Drag(PointerEventData eventData)
        {
            _ray = GetRayFromUI(eventData);

            if (Physics.Raycast(_ray, out RaycastHit hitInfo, Mathf.Infinity, _layerMask)
                && hitInfo.collider.TryGetComponent(out Ground _))
            {
                _indicatorInstance.transform.position = hitInfo.point;
            }
        }

        public Vector3 EndDrag()
        {
            Vector3 positionToSpawn = _indicatorInstance.transform.position;
            _indicatorInstance.gameObject.SetActive(false);
            _indicatorInstance.transform.position = DefaultPosition;

            return positionToSpawn;
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