using Bootstrap;
using CodeExtensions;
using DG.Tweening;
using Reflex.Attributes;
using TMPro;
using UnityEngine;

namespace SearchOpponent
{
    [RequireComponent(typeof(TMP_Text))]
    internal class SearchingOpponentTextView : MonoBehaviour
    {
        private const int InfiniteLoops = -1;
        private const string AppendText = "...";

        private readonly Ease _ease = Ease.OutQuad;

        [SerializeField] private float _animationDuration = 0.5f;
        [SerializeField] private string _localizationKey = "Searching";
        [SerializeField] private string _format = "{0}";
        
        private TMP_Text _textField;
        private Sequence _sequence;
        private SavvyServicesProvider _services;

        [Inject]
        private void Initialize(SavvyServicesProvider servicesProvider) =>
            _services = servicesProvider;

        private void Awake() =>
            _textField = GetComponent<TMP_Text>();

        private void OnEnable()
        {
            _services.Localisation.LocalizationUpdated += UpdateView;
            UpdateView();
        }

        private void OnDisable()
        {
            _sequence?.Kill();
            _services.Localisation.LocalizationUpdated -= UpdateView;
        }

        private void CreateAnimation()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            _sequence.SetEase(_ease);
            _sequence.Append(_textField.DOText(AppendText, _animationDuration).SetRelative());
            _sequence.SetLoops(InfiniteLoops, LoopType.Restart);
        }

        private void UpdateView()
        {
            _textField.text = string.Format(_format, _services.Localisation.GetTranslation(_localizationKey));
            CreateAnimation();
        }
    }
}