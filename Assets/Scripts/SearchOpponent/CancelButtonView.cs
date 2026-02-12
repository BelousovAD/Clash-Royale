using UnityEngine;

namespace SearchOpponent
{
    internal class CancelButtonView : MonoBehaviour
    {
        [SerializeField] private FakeOpponentSearch _opponentSearch;
        [SerializeField] private CancelButton _button;

        private void OnEnable()
        {
            _opponentSearch.SearchStatusChanged += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _opponentSearch.SearchStatusChanged -= UpdateView;

        private void UpdateView() =>
            _button.gameObject.SetActive(_opponentSearch.IsFound == false);
    }
}