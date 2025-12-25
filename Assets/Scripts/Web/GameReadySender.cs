namespace Web
{
    using MirraGames.SDK;
    using UnityEngine;

    public class GameReadySender : MonoBehaviour
    {
        private static GameReadySender _instance;

        private void Awake()
        {
            if (_instance is null)
            {
                _instance = this;
                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start() =>
            MirraSDK.Analytics.GameIsReady();
    }
}