using UnityEngine;
using UnityEngine.SceneManagement;

namespace SearchOpponent
{
    internal class GameSceneLoader : MonoBehaviour
    {
        private const string GameSceneName = "Game";

        [SerializeField] private FakeOpponentSearch _opponentSearch;

        private void OnEnable() =>
            _opponentSearch.SearchStatusChanged += Load;

        private void OnDisable() =>
            _opponentSearch.SearchStatusChanged -= Load;

        private void Load()
        {
            if (_opponentSearch.IsFound)
            {
                SceneManager.LoadSceneAsync(GameSceneName);
            }
        }
    }
}