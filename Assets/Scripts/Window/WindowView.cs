using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Window
{
    [RequireComponent(typeof(CanvasGroup))]
    internal class WindowView : MonoBehaviour
    {
        private static readonly Vector3 MinScale = Vector3.zero;
        private static readonly Vector3 MaxScale = Vector3.one;

        [SerializeField] private Window _window;
        [SerializeField] private RectTransform _content;
        [SerializeField][Min(0f)] private float _animationDuration;

        private CanvasGroup _canvasGroup;
        private Tweener _tweener;
        private Coroutine _changeActivity;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _window.ActivityChanged += UpdateActivity;
            _window.InteractableChanged += UpdateInteractable;
        }

        private void OnDestroy()
        {
            _window.ActivityChanged -= UpdateActivity;
            _window.InteractableChanged -= UpdateInteractable;
            _tweener.Kill(true);
        }

        private void UpdateActivity()
        {
            if (_window.IsActive)
            {
                transform.SetAsLastSibling();
                _content.localScale = MinScale;
                _canvasGroup.interactable = false;
                gameObject.SetActive(true);
            }

            _changeActivity = StartCoroutine(ChangeActivityRoutine());
        }

        private IEnumerator ChangeActivityRoutine()
        {
            _tweener.Kill(true);

            if (_window.IsActive)
            {
                _tweener = _content.DOScale(MaxScale, _animationDuration)
                    .SetUpdate(true)
                    .OnComplete(() => _canvasGroup.interactable = _window.IsInteractable);
            }
            else
            {
                _tweener = _content.DOScale(MinScale, _animationDuration)
                    .SetUpdate(true)
                    .OnComplete(() => gameObject.SetActive(false));
            }

            yield return _tweener.WaitForCompletion();

            _changeActivity = null;
        }

        private void UpdateInteractable()
        {
            if (_changeActivity == null)
            {
                _canvasGroup.interactable = _window.IsInteractable;
            }
        }
    }
}