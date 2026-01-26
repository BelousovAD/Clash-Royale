using Character;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace CardBattle
{
    public class PointerIndicator : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Indicator _indicatorPrefab;
        [SerializeField] private LayerMask _layerMask;

        private RawImage _rawImage;
        private Ray _ray;
        private Indicator _prefab;

        private void Start()
        {
            _rawImage = FindFirstObjectByType<RawImage>();
        }

        public void OnBeginDrag()
        {
            _ray = GetRayFromUI();
            
            if (Physics.Raycast(_ray, out RaycastHit hitInfo, Mathf.Infinity, _layerMask))
            {
                if (hitInfo.collider.TryGetComponent<Ground>(out Ground basket))
                {
                    _prefab = Instantiate(_indicatorPrefab);
                }
            }
        }

        public void OnDrag()
        {
            _ray = GetRayFromUI();

            if (Physics.Raycast(_ray, out RaycastHit hitInfo, Mathf.Infinity, _layerMask))
            {
                if (hitInfo.collider.TryGetComponent<Ground>(out Ground basket))
                {
                    Vector3 newMovePosition = new Vector3(hitInfo.point.x, hitInfo.point.y, hitInfo.point.z);
                    _prefab.transform.position = newMovePosition;
                }
            }
        }

        public void OnEndDrag()
        {
            Destroy(_prefab.gameObject);
        }

        private Ray GetRayFromUI()
        {
            RectTransform rectTransform = _rawImage.rectTransform;

            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform,
                Input.mousePosition,
                null,
                out localPoint);

            Rect rect = rectTransform.rect;

            float normalizedX = (localPoint.x - rect.x) / rect.width;
            float normalizedY = (localPoint.y - rect.y) / rect.height;

            return _camera.ViewportPointToRay(new Vector3(normalizedX, normalizedY, 0));
        }
    }
}