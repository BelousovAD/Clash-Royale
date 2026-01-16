using System.Collections.Generic;
using Reflex.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Reward
{
    internal class RewarderView : MonoBehaviour
    {
        [SerializeField] private RewardType _type;
        [Header("Behaviour")]
        [SerializeField] private bool _showReward = true;
        [SerializeField] private bool _showPenalty = true;
        [Header("Icon")]
        [SerializeField] private Image _image;
        [Header("Text")]
        [SerializeField] private TMP_Text _textField;
        [SerializeField] private string _rewardFormat = "+{0}";
        [SerializeField] private string _penaltyFormat = "-{0}";

        private Rewarder Rewarder { get; set; }

        [Inject]
        private void Initialize(IEnumerable<Rewarder> rewarders)
        {
            foreach (Rewarder rewarder in rewarders)
            {
                if (rewarder.Type == _type)
                {
                    Rewarder = rewarder;
                    break;
                }
            }
        }

        protected virtual void Awake() =>
            Rewarder.Rewarded += UpdateView;

        protected virtual void OnDestroy() =>
            Rewarder.Rewarded -= UpdateView;

        protected virtual void Start() =>
            UpdateView();

        private void UpdateView()
        {
            switch (Rewarder.IsVictory)
            {
                case true:
                    gameObject.SetActive(_showReward);
                    UpdateReward();
                    break;
                case false:
                    gameObject.SetActive(_showPenalty);
                    UpdatePenalty();
                    break;
            }
        }

        private void UpdatePenalty()
        {
            _image.sprite = Rewarder.Icon;
            _textField.text = string.Format(_penaltyFormat, Rewarder.LoseAmount);
        }

        private void UpdateReward()
        {
            _image.sprite = Rewarder.Icon;
            _textField.text = string.Format(_rewardFormat, Rewarder.WinAmount);
        }
    }
}