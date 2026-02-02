using TMPro;
using UnityEngine;

namespace Chest
{
    internal class ChestBonusPriceView : MonoBehaviour
    {
        [SerializeField] private GameObject _priceAdView;
        [SerializeField] private TMP_Text _textField;

        private ChestBonusCalculator _bonusCalculator;

        protected void Awake() =>
            _bonusCalculator = GetComponent<ChestBonusCalculator>();

        private void OnEnable() =>
            _bonusCalculator.Calculated += UpdateView;

        private void OnDisable() =>
            _bonusCalculator.Calculated -= UpdateView;

        private void UpdateView()
        {
            _textField.text = _bonusCalculator.BonusPrice.ToString();

            if (_bonusCalculator.IsMoneyEnough == false || _bonusCalculator.BonusPrice == 0)
            {
                _priceAdView.SetActive(true);
                _textField.color = Color.red;
            }
            else
            {
                _priceAdView.SetActive(false);
                _textField.color = Color.white;
            }
        }
    }
}