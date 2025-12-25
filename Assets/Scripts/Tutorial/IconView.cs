namespace Tutorial
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Image))]
    public class IconView : MonoBehaviour
    {
        [SerializeField] private Sprite _defaultIcon;
        [SerializeField] private Tutorial _tutorial;

        private Image _image;

        private void Awake() =>
            _image = GetComponent<Image>();

        private void Start() =>
            _image.sprite = _tutorial.Icon;
    }
}