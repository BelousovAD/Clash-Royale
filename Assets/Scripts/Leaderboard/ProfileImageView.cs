namespace Leaderboard
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Image))]
    internal class ProfileImageView : MonoBehaviour
    {
        [SerializeField] private LeaderboardItem _item;
        [SerializeField] private Sprite _loading;

        private Image _image;

        private void Awake() =>
            _image = GetComponent<Image>();

        private void OnEnable()
        {
            _item.IconChanged += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _item.IconChanged -= UpdateView;

        private void UpdateView() =>
            _image.sprite = _item.Icon ?? _loading;
    }
}
