using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Leaderboard
{
    internal class ProfileIconLoader : MonoBehaviour
    {
        private static readonly Dictionary<string, Texture2D> _textures = new ();

        [SerializeField] private LeaderboardItem _item;
        [SerializeField] private Sprite _default;

        private void OnEnable()
        {
            _item.Initialized += UpdateTexture;
            UpdateTexture();
        }

        private void OnDisable() =>
            _item.Initialized -= UpdateTexture;

        private void UpdateTexture() =>
            SetTexture(_item.IconUrl);

        private void SetTexture(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                _item.SetIcon(_default);
                return;
            }

            if (_textures.TryGetValue(url, out Texture2D texture))
            {
                SetTexture(texture);
            }
            else
            {
                StartCoroutine(LoadTexture(url));
            }
        }

        private void SetTexture(Texture2D texture)
        {
            Rect rect = new (0, 0, texture.width, texture.height);
            _item.SetIcon(Sprite.Create(texture, rect, Vector2.zero));
        }

        private IEnumerator LoadTexture(string url)
        {
            using UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url);

            yield return webRequest.SendWebRequest();

            if (webRequest.result is UnityWebRequest.Result.ConnectionError
                or UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogError(webRequest.error);
                _item.SetIcon(_default);
                yield break;
            }

            DownloadHandlerTexture downloadHandler = webRequest.downloadHandler as DownloadHandlerTexture;

            if (!downloadHandler!.isDone)
            {
                _item.SetIcon(_default);
                yield break;
            }

            _textures.Add(url, downloadHandler.texture);
            SetTexture(downloadHandler.texture);
        }
    }
}