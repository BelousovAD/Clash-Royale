using Character;
using UnityEngine;
using UnityEngine.UI;

namespace CardBattle
{
    public class PointerIndicator
    {
        private static readonly Vector3 DefaultPosition = new Vector3(0, -5, 0);

        private readonly LayerMask _layerMask;
        private readonly Indicator _indicatorPrefab;
        private readonly Camera _camera;

        private RawImage _rawImage;
        private Ray _ray;

        public PointerIndicator(LayerMask layerMask, Indicator indicator, Camera camera)
        {
            _layerMask = layerMask;
            _indicatorPrefab = indicator;
            _camera = camera;
        }

        public void Initialize(RawImage image) =>
            _rawImage = image;

        public void BeginDrag() =>
            _indicatorPrefab.gameObject.SetActive(true);

        public void Drag()
        {
            _ray = GetRayFromUI();

            if (Physics.Raycast(_ray, out RaycastHit hitInfo, Mathf.Infinity, _layerMask)
                && hitInfo.collider.TryGetComponent<Ground>(out Ground basket))
            {
                _indicatorPrefab.transform.position = hitInfo.point;
            }
        }

        public void EndDrag()
        {
            _indicatorPrefab.gameObject.SetActive(false);
            _indicatorPrefab.transform.position = DefaultPosition;
        }

        private Ray GetRayFromUI()
        {
            RectTransform rectTransform = _rawImage.rectTransform;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform,
                Input.mousePosition,
                null,
                out Vector2 localPoint);

            Rect rect = rectTransform.rect;

            float normalizedX = (localPoint.x - rect.x) / rect.width;
            float normalizedY = (localPoint.y - rect.y) / rect.height;

            return _camera.ViewportPointToRay(new Vector3(normalizedX, normalizedY, 0));
        }
    }
}